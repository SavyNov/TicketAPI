using AutoMapper;
using TicketAPI.Contracts.Ticket;
using TicketAPI.Contracts.User;

namespace TicketAPI.Profiles {
    public class MappingProfile : Profile{
        public MappingProfile() {
            CreateMap<Ticket, InputTicketDTO>();
            CreateMap<Ticket, ViewTicketDTO>();
            CreateMap<InputTicketDTO, Ticket>();
            CreateMap<ViewTicketDTO, Ticket>();
            CreateMap<User, InputUserDTO>();
            CreateMap<User, LoginUserDTO>();
            CreateMap<InputUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<LoginUserDTO, User>();


            CreateMap<UpdateUserDTO, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember!=null));


            /*CreateMap<InputTicketDTO, Ticket>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember!=null));*/

            CreateMap<InputTicketDTO, Ticket>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => {
                    if (srcMember==null)
                        return false;

                    // For value types (like int, bool), skip default values if the DTO doesn't contain a real change.
                    if (srcMember is int&&(int)srcMember==default(int))
                        return false;

                    if (srcMember is double&&(double)srcMember==default(double))
                        return false;

                    // Add more checks if needed (e.g., DateTime).
                    if (srcMember is DateTime&&(DateTime)srcMember==default(DateTime))
                        return false;

                    return true;
                }));
 
            CreateMap<User, ViewUserDTO>()
                .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets));
        }
    }
}
