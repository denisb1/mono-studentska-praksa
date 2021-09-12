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
	public class TeacherService : IGenericService<TeacherRest, CreateTeacherRest, UpdateTeacherRest>
	{
		private readonly IRepositoryWork _repositoryWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Teacher> _repo;

		public TeacherService(IRepositoryWork repositoryWork, IMapper mapper)
		{
			_repositoryWork = repositoryWork;
			_mapper = mapper;
			_repo = _repositoryWork.TeacherRepository;
		}

		public async Task<TeacherRest> GetById(Guid id)
		{
			var teacher = await _repo
				.Get(x => x.Id == id, new List<string> { "Courses" });
			return _mapper.Map<TeacherRest>(teacher);
		}

		private async Task<IList<TeacherRest>> GetAll()
		{
			var teachers = await _repo.GetAll(null, null, new List<string> { "Courses" });
			return _mapper.Map<IList<TeacherRest>>(teachers);
		}

		public async Task<IList<TeacherRest>> GetAll(string @where, string order)
		{
			Func<IQueryable<Teacher>, IOrderedQueryable<Teacher>> orderBy = null;
			Expression<Func<Teacher, bool>> expression = null;

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

		public async Task Insert(CreateTeacherRest teacher)
		{
			var newTeacher = _mapper.Map<Teacher>(teacher);
			newTeacher.Id = Guid.NewGuid();
			await _repo.Insert(newTeacher);
			await _repositoryWork.Save();
		}

		public async Task Update(Guid id, UpdateTeacherRest newTeacher)
		{
			var oldTeacher = await _repo.Get(q => q.Id == id);
			if (oldTeacher == null) throw new ArgumentException("Doesn't exist.");

			if (!string.IsNullOrEmpty(newTeacher.Department)) oldTeacher.Department = newTeacher.Department;
			if (!string.IsNullOrEmpty(newTeacher.FirstName)) oldTeacher.FirstName = newTeacher.FirstName;
			if (!string.IsNullOrEmpty(newTeacher.LastName)) oldTeacher.LastName = newTeacher.LastName;

			_repo.Update(oldTeacher);
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
