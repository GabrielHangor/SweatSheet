using AutoMapper;
using SweatSheet.Server.Modules.Exercise.DTOs;
using SweatSheet.Server.Modules.Exercise.Entities;
using SweatSheet.Server.Modules.Workouts.DTOs;
using SweatSheet.Server.Modules.Workouts.Entities;

namespace SweatSheet.Server;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Workout, WorkoutDto>();
        CreateMap<WorkoutRequestDto, Workout>().ForMember(w => w.Activities, opt => opt.Ignore());
        CreateMap<WorkoutActivityRequestDto, WorkoutActivity>();
        CreateMap<ExerciseCreateRequestDto, Exercise>();
    }
}
