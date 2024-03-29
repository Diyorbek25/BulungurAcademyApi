﻿using BulungurAcademy.Domain.Entities.Common;
using BulungurAcademy.Domain.Entities.Exams;
using BulungurAcademy.Domain.Entities.Subjects;
using BulungurAcademy.Domain.Entities.Users;
using BulungurAcademy.Domain.Enum;

namespace BulungurAcademy.Domain.Entities;

public class ExamApplicant : Auditable
{
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }
    public Guid? FirstSubjectId { get; set; }
    public Guid? SecondSubjectId { get; set;}
    public bool? IsArrived { get; set; } = false;
    public bool? IsPayed { get; set; } = false;
    public User User { get; set; }
    public Exam Exam { get; set; }
    public Subject FirstSubject { get; set; }
    public Subject SecondSubject { get; set; }
}
