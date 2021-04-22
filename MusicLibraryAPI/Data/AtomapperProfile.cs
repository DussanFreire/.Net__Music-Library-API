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
        /*
          this.CreateMap<TeamModel, TeamEntity>()
                //.ForMember(tm => tm.Name, te => te.MapFrom(m => m.Name))
                .ReverseMap();

            this.CreateMap<PlayerModel, PlayerEntity>()
                .ForMember(ent => ent.Team, mod => mod.MapFrom(modSrc => new TeamEntity() { Id = modSrc.TeamId }))
                .ReverseMap()
                .ForMember(mod => mod.TeamId, ent => ent.MapFrom(entSrc => entSrc.Team.Id));

            this.CreateMap<TeamWithPlayerModel, TeamEntity>()
                .ReverseMap();
         */
        public AtomapperProfile()
        {
            this.CreateMap<ArtistModel, ArtistEntity>().ReverseMap();
            this.CreateMap<AlbumModel, AlbumEntity>()
            .ForMember(ent => ent.Artist, mod => mod.MapFrom(modSrc => new ArtistEntity() { Id = modSrc.ArtistId }))
            .ReverseMap()
            .ForMember(mod => mod.ArtistId, ent => ent.MapFrom(entSrc => entSrc.Artist.Id)); 
            this.CreateMap<SongModel, SongEntity>()
            .ForMember(ent => ent.Album, mod => mod.MapFrom(modSrc => new AlbumEntity() { Id = modSrc.AlbumId }))
            .ReverseMap()
            .ForMember(mod => mod.AlbumId, ent => ent.MapFrom(entSrc => entSrc.Album.Id));
        }
    }
}
