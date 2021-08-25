using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Day6.DAL;
using Day6.Models.DTO;
using Day6.Models.REST;
using Day6.Repository;

namespace Day6.Service
{
	public class StudentService : IGenericService<StudentRest>
	{
		private readonly IRepositoryWork _repositoryWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<StudentDb> _repo;

		public StudentService(IRepositoryWork repositoryWork, IMapper mapper)
		{
			_repositoryWork = repositoryWork;
			_mapper = mapper;
			_repo = repositoryWork.StudentDbRepository;
		}

		public async Task<StudentRest> GetById(Guid id)
		{
			var student = await _repo
				.Get(x => x.Id == id);
			return _mapper.Map<StudentRest>(student);
		}

		private async Task<IList<StudentDto>> GetAll()
		{
			var students = await _repo.GetAll();
			return _mapper.Map<IList<StudentDto>>(students);
		}

		public async Task<IList<StudentRest>> GetAll(string where, string order)
		{
			Func<IQueryable<StudentDb>, IOrderedQueryable<StudentDb>> orderBy = null;
			Expression<Func<StudentDb, bool>> expression = null;

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

		public async Task Insert(StudentRest student)
		{
			await _repo.Insert(_mapper.Map<StudentDb>(student));
			await _repositoryWork.Save();
		}

		public async Task Update(Guid id, StudentRest newStudent)
		{
			var oldStudent = await _repo.Get(q => q.Id == id);
			if (oldStudent == null) throw new ArgumentException("Doesn't exist.");

			_mapper.Map(newStudent, oldStudent);
			_repo.Update(oldStudent);
			await _repositoryWork.Save();
		}

		public async Task Delete(StudentRest student)
		{
			var students = await _repo.GetAll(q =>
				q.FirstName == student.FirstName &&
				q.LastName == student.LastName &&
				q.Year == student.Age &&
				q.Age == student.Age
			);

			if (students == null) throw new ArgumentException("Doesn't exist.");
			_repo.DeleteRange(students);
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
