﻿using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.CreateNode;

namespace Notes.WebApi.Models;

public class CreateNoteDto : IMapWith<CreateNoteCommand>
{
    public string Title { get; set; }
    public string Details { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
            .ForMember(command => command.Title,
                opt => opt.MapFrom(noteDto => noteDto.Title))
            .ForMember(command => command.Details,
                opt => opt.MapFrom(noteDto => noteDto.Details));
    }
}