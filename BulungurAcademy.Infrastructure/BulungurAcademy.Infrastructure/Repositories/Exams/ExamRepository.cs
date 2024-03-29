﻿using BulungurAcademy.Domain.Entities.Exams;
using BulungurAcademy.Infrastructure.Contexts;

namespace BulungurAcademy.Infrastructure.Repositories.Exams;

public class ExamRepository : Repository<Exam>, IExamRepository
{
	public ExamRepository(AppDbContext appDbContext)
		: base(appDbContext)
	{

	}
}
