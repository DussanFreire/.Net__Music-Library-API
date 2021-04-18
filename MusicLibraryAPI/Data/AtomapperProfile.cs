using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicLibraryAPI.Models;
using MusicLibraryAPI.Data.Entities;
using AutoMapper;

namespace MusicLibraryAPI.Data
{
    public class AtomapperProfile : Profile
    {
        /*
        this.CreateMap<TeamModel, TeamEntity>()
                //.ForMember(tm => tm.Name, te => te.MapFrom(m => m.Name))
                .ReverseMap();

            this.CreateMap<PlayerModel, PlayerEntity>()
                .ReverseMap();

            this.CreateMap<TeamWithPlayerModel, TeamEntity>()
                .ReverseMap();*/
        public AtomapperProfile()
        {
            this.CreateMap<ArtistModel, ArtistEntity>().ReverseMap();
            this.CreateMap<AlbumModel, AlbumEntity>().ReverseMap();
            this.CreateMap<SongModel, SongEntity>().ReverseMap();
        }
    }
}
