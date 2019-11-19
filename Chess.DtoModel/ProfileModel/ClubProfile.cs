using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            this.CreateMap<ClubMember, ClubMemberViewModel>()
            .ConvertUsing<ByteStringConverter>();

            this.CreateMap<Club, ClubViewModel>()
                        .ForMember(d => d.Name, s => s.MapFrom(x => x.ClubName))
                        .ForMember(d => d.RoomMoney, s => s.MapFrom(x => x.PayMoney))
                        .ForMember(d => d.Rate, s => s.MapFrom(x => x.Rate))
                        //.ForMember(d => d.ShortUrl, s => s.MapFrom(x => x.ShortLink))
                        .ForMember(d => d.Id, s => s.MapFrom(x => x.ClubId))
                        .ForMember(d => d.PayImage, s => s.MapFrom(x => x.PayImage))
                        .ForMember(d => d.ShortUrl, s => s.Ignore())
                        .ForMember(d => d.ClubMemberViews, s => s.Ignore());
        }
    }
}
