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
	public class TeacherService : IGenericService<TeacherRest>
	{
		private readonly IRepositoryWork _repositoryWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<TeacherDb> _repo;

		public TeacherService(IRepositoryWork repositoryWork, IMapper mapper)
		{
			_repositoryWork = repositoryWork;
			_mapper = mapper;
			_repo = _repositoryWork.TeacherDbRepository;
		}

		public async Task<TeacherRest> GetById(Guid id)
		{
			var teacher = await _repo
				.Get(x => x.Id == id);
			return _mapper.Map<TeacherRest>(teacher);
		}

		private async Task<IList<TeacherDto>> GetAll()
		{
			var teachers = await _repo.GetAll();
			return _mapper.Map<IList<TeacherDto>>(teachers);
		}

		public async Task<IList<TeacherRest>> GetAll(string @where, string order)
		{
			Func<IQueryable<TeacherDb>, IOrderedQueryable<TeacherDb>> orderBy = null;
			Expression<Func<TeacherDb, bool>> expression = null;

			if (where != null)expression = x =>
				x.FirstName.ToLower().Contains(where.ToLower()) ||
				x.LastName.ToLower().Contains(where.ToLower()) ||
				x.Department.ToLower().Contains(where.ToLower());

			if (order != null)
				orderBy = order.ToLower() switch
				{
					"firstname" => x => x.OrderBy(y => y.FirstName),
					"firstname_desc" => x => x.OrderByDescending(y => y.FirstName),
					"lastname" => x => x.OrderBy(y => y.LastName),
					"lastname_desc" => x => x.OrderByDescending(y => y.LastName),
					"department" => x => x.OrderBy(y => y.Department),
					"department_desc" => x => x.OrderByDescending(y => y.Department),
					_ => null
				};

			if (where == null && order == null)
				return _mapper.Map<IList<TeacherRest>>(await GetAll());

			var courses = await _repo.GetAll(expression, orderBy);
			return _mapper.Map<IList<TeacherRest>>(courses);
		}

		public async Task Insert(TeacherRest teacher)
		{
			await _repo.Insert(_mapper.Map<TeacherDb>(teacher));
			await _repositoryWork.Save();
		}

		public async Task Update(Guid id, TeacherRest newTeacher)
		{
			var oldTeacher = await _repo.Get(q => q.Id == id);
			if (oldTeacher == null) throw new ArgumentException("Doesn't exist.");

			_mapper.Map(newTeacher, oldTeacher);
			_repo.Update(oldTeacher);
			await _repositoryWork.Save();
		}

		public async Task Delete(TeacherRest teacher)
		{
			var teachers = await _repo.GetAll(q =>
				q.FirstName == teacher.FirstName &&
				q.LastName == teacher.LastName &&
				q.Department == teacher.Department
			);

			if (teachers == null) throw new ArgumentException("Doesn't exist.");
			_repo.DeleteRange(teachers);
			await _repositoryWork.Save();
		}

		public async Task Delete(Guid id)
		{
			var teacher = await _repo.Get(x => x.Id == id);
			if (teacher == null) throw new ArgumentException("Doesn't exist.");
			await _repo.Delete(id);
			await _repositoryWork.Save();
		}
	}
}
