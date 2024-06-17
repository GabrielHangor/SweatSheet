using AutoMapper;

namespace SweatSheet.Server;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<WorkoutRequestDto, Workout>().ForMember(dest => dest.Activities, opt => opt.Ignore());
        CreateMap<WorkoutActivityRequestDto, WorkoutActivity>();
        CreateMap<ExerciseCreateRequestDto, Exercise>();
    }
}