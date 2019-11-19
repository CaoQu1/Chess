using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    public class ValueTypeConverter : ITypeConverter<string, int>
    {
        public int Convert(string source, int destination, ResolutionContext context)
        {
            destination = int.Parse(source);
            return destination;
        }
    }

    public class ByteStringConverter : ITypeConverter<ClubMember, ClubMemberViewModel>
    {
        public ClubMemberViewModel Convert(ClubMember source, ClubMemberViewModel destination, ResolutionContext context)
        {
            if (source.SystemUser != null)
            {
                destination = destination ?? new ClubMemberViewModel();
                destination.NickName = source.SystemUser.NickName;
                if (!string.IsNullOrEmpty(source.SystemUser.PayCodeUrl))
                {
                    destination.PayImage = source.SystemUser.PayCodeUrl;
                }
                if (!string.IsNullOrEmpty(source.SystemUser.HeadImgUrl))
                {
                    destination.HeadImage = source.SystemUser.HeadImgUrl;
                }
                destination.UserId = source.SystemUser.UserId.HasValue ? (int)source.SystemUser.UserId : 0;
                destination.GameId = source.SystemUser.GameId.HasValue ? (int)source.SystemUser.GameId : 0;
            }
            return destination;
        }
    }
}
