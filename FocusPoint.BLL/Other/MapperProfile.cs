using AutoMapper;
using FocusPoint.BLL.Models;
using FocusPoint.DAL.Entities;

namespace FocusPoint.BLL.Other;

public class MapperProfile : Profile
{
    public MapperProfile()
    {

        CreateMap<MainNote, MainNoteDto>()
            .ReverseMap();

        // Mapping для TaskItem
        CreateMap<TaskItem, TaskItemDto>()
            .ReverseMap();

        // Mapping для WorkSession
        CreateMap<WorkSession, WorkSessionDto>()
            .ReverseMap();

        // Mapping для WorkStatistic
        CreateMap<WorkStatistic, WorkStatisticDto>()
            .ReverseMap();

        // Mapping для SavedNote
        CreateMap<SavedNote, SavedNoteDto>()
            .ReverseMap();


        CreateMap<User, UserDto>()
            .ForMember(dest => dest.TaskIds, opt => opt.MapFrom(src => src.Tasks.Select(t => t.Id)))
            .ForMember(dest => dest.MainNoteIds, opt => opt.MapFrom(src => src.MainNotes.Select(mn => mn.Id)))
            .ForMember(dest => dest.WorkSessionIds, opt => opt.MapFrom(src => src.WorkSessions.Select(ws => ws.Id)))
            .ForMember(dest => dest.WorkStatisticIds, opt => opt.MapFrom(src => src.WorkStatistics.Select(wst => wst.Id)))
            .ForMember(dest => dest.SavedNoteIds, opt => opt.MapFrom(src => src.SavedNotes.Select(sn => sn.Id)))
            .ReverseMap();


    }
}