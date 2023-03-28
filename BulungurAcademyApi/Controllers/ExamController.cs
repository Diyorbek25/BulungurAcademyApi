﻿using BulungurAcademy.Application.DataTranferObjects.Exams;
using BulungurAcademy.Application.Services.Exams;
using Microsoft.AspNetCore.Mvc;

namespace BulungurAcademy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamController : ControllerBase
{
    private readonly IExamService service;
    public ExamController(IExamService examService)
        => this.service = examService;

    [HttpPost]
    public async Task<IActionResult> CreateExamsAsync(ExamForCreationDto exam)
    {
        var creationExam = await this.service.CreateExamAsync(exam);

        return Ok(creationExam);
    }
    [HttpPost("examId:Guid/subjectId:Guid")]
    public async Task<IActionResult> AddExamSubjec(
        Guid examId,
        Guid subjectId)
    {
        var exam = await service.CreateExamSubject(examId, subjectId);
        return Ok(exam);
    }
    [HttpGet]
    public IActionResult GetExams()
    {
        var exams = service.RetrieveExams();

        return Ok(exams);
    }
      
    [HttpGet("examId:Guid")]
    public async Task<IActionResult> GetExamByIdAsync(Guid examId)
    {
        var exam = await service.RetrieveExamByIdAsync(id: examId);

        return Ok(exam);
    }

    [HttpGet("examWithDetalias:Guid")]
    public async Task<IActionResult> GetExamWithDetailsAsync(Guid id)
    {
        var examWithDetalias = await this.service.RetrieveExamWithDetailsAsync(id);

        return Ok(examWithDetalias);
    }


    [HttpPut]
    public async Task<IActionResult> PutExamAsync(ExamForModificationDto exam)
    {
        var modifyExam = await this.service.ModifyExamAsync(exam);

        return Ok(modifyExam);
    }
    [HttpDelete("{examId:guid}")]
    public async Task<IActionResult> DeleteExamAsync(Guid examId)
    {
        var deleteExam = await this.service.RemoveExamAsync(examId);

        return Ok(deleteExam);
    }
}