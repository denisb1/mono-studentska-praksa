using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using day9.DAL;
using day9.Model;
using day9.Repository.Common;
using day9.Service.Common;

namespace day9.Service
{
	public class StudentService : IGenericService<StudentRest, CreateStudentRest, UpdateStudentRest>
	{
		private readonly IRepositoryWork _repositoryWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Student> _repo;

		public StudentService(IRepositoryWork repositoryWork, IMapper mapper)
		{
			_repositoryWork = repositoryWork;
			_mapper = mapper;
			_repo = repositoryWork.StudentRepository;
		}

		public async Task<StudentRest> GetById(Guid id)
		{
			var student = await _repo
				.Get(x => x.Id == id);
			return _mapper.Map<StudentRest>(student);
		}

		private async Task<IList<StudentRest>> GetAll()
		{
			var students = await _repo.GetAll(null, null, new List<string> { "Enrollments.Course" });
			return _mapper.Map<IList<StudentRest>>(students);
		}

		public async Task<IList<StudentRest>> GetAll(string where, string order)
		{
			Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null;
			Expression<Func<Student, bool>> expression = null;

			if (where != null) expression = x =>
				x.FirstName.ToLower().Contains(where.ToLower()) ||
				x.LastName.ToLower().Contains(where.ToLower());

			if (order != null)
				orderBy = order.ToLower() switch
				{
					"firstname" => x => x.OrderBy(y => y.FirstName),
					"firstname_desc" => x => x.OrderByDescending(y => y.FirstName),
					"lastname" => x => x.OrderBy(y => y.LastName),
					"lastname_desc" => x => x.OrderByDescending(y => y.LastName),
					"age" => x => x.OrderBy(y => y.Age),
					"age_desc" => x => x.OrderByDescending(y => y.Age),
					"year" => x => x.OrderBy(y => y.Year),
					"year_desc" => x => x.OrderByDescending(y => y.Year),
					_ => null
				};

			if (where == null && order == null)
				return _mapper.Map<IList<StudentRest>>(await GetAll());

			var students = await _repo.GetAll(expression, orderBy);
			return _mapper.Map<IList<StudentRest>>(students);
		}

		public async Task Insert(CreateStudentRest student)
		{
			var newStudent = _mapper.Map<Student>(student);
			await _repo.Insert(newStudent);
			await _repositoryWork.Save();
		}

		public async Task Update(Guid id, UpdateStudentRest newStudent)
		{
			var oldStudent = await _repo.Get(q => q.Id == id);
			if (oldStudent == null) throw new ArgumentException("Doesn't exist.");

			_mapper.Map(newStudent, oldStudent);
			_repo.Update(oldStudent);
			await _repositoryWork.Save();
		}

		public async Task Delete(Guid id)
		{
			var student = await _repo.Get(x => x.Id == id);
			if (student == null) throw new ArgumentException("Doesn't exist.");
			await _repo.Delete(id);
			await _repositoryWork.Save();
		}
	}
}
