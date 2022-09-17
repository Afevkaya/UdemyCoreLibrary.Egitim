﻿using AutoMapper;
using FluentValidationApp.DTOs;
using FluentValidationApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationApp.Controllers;

public class ProjectionController : Controller
{
    private readonly IMapper _mapper;
    // GET
    public ProjectionController(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(EventDateDto eventDateDto)
    {
        EventDate eventDate = _mapper.Map<EventDate>(eventDateDto);
        ViewBag.date = eventDate.Date.ToShortDateString();
        return View();
    }
}