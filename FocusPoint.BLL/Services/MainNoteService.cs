using AutoMapper;
using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories;
using FocusPoint.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FocusPoint.BLL.Services;

public class MainNoteService: IMainNoteService
{

    private readonly IMainNoteRepository _mainNoteRepository;
    private readonly IMapper _mapper;

    public MainNoteService(IMainNoteRepository mainNoteRepository, IMapper mapper)
    {
        _mainNoteRepository = mainNoteRepository;
        _mapper = mapper;
    }


    public async Task<MainNoteDto?> GetByIdAsync(Guid mainNodeId)
    {
        var mainNote = await _mainNoteRepository.GetByIdAsync(mainNodeId);

        return _mapper.Map<MainNoteDto>(mainNote);
    }

    public async Task AddAsync(MainNoteDto mainNoteDto)
    {
        var mainNote = _mapper.Map<MainNote>(mainNoteDto);

        await _mainNoteRepository.AddAsync(mainNote);
    }

    public async Task UpdateAsync(MainNoteDto mainNoteDto)
    {
        var mainNote = _mapper.Map<MainNote>(mainNoteDto);

        await _mainNoteRepository.UpdateAsync(mainNote);
    }

    public async Task<bool> DeleteAsync(Guid mainNodeId)
    {
        var mainNote = await _mainNoteRepository.GetByIdAsync(mainNodeId);

        if (mainNote is null)
        {
            return false;
        }

        await _mainNoteRepository.DeleteAsync(mainNote);
        return true;
    }

    public async Task<List<MainNoteDto>> GetAllByUserIdOrdered(Guid userId)
    {
        var mainNotes = await _mainNoteRepository.GetAllByUserIdOrdered(userId);

        return _mapper.Map<List<MainNoteDto>>(mainNotes);
    }

    public async Task UpdateManyAsync(IEnumerable<MainNoteDto> notesToUpdateDtos)
    {
        var notesToUpdate = _mapper.Map<List<MainNote>>(notesToUpdateDtos);

        await _mainNoteRepository.UpdateManyAsync(notesToUpdate);
    }


    public async Task ReorderNotesAsync(List<MainNote> notes)
    {
        for (int i = 0; i < notes.Count; i++)
            notes[i].OrderIndex = i;

        await _mainNoteRepository.UpdateManyAsync(notes);
    }


}