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
	public class CourseService : IGenericService<CourseRest, CreateCourseRest, UpdateCourseRest>
	{
		private readonly IRepositoryWork _repositoryWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<Course> _repo;

		public CourseService(IRepositoryWork repositoryWork, IMapper mapper)
		{
			_repositoryWork = repositoryWork;
			_mapper = mapper;
			_repo = _repositoryWork.CourseRepository;
		}

		public async Task<CourseRest> GetById(Guid id)
		{
			var course = await _repo
				.Get(x => x.Id == id);
			return _mapper.Map<CourseRest>(course);
		}

		private async Task<IList<CourseRest>> GetAll()
		{
			var students = await _repo.GetAll();
			return _mapper.Map<IList<CourseRest>>(students);
		}

		public async Task<IList<CourseRest>> GetAll(string where, string order)
		{
			Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null;
			Expression<Func<Course, bool>> expression = null;

			if (where != null) expression = x => x.CourseName.ToLower().Contains(where.ToLower());
			if (order != null)
				orderBy = order.ToLower() switch
				{
					"coursename" => x => x.OrderBy(y => y.CourseName),
					"coursename_desc" => x => x.OrderByDescending(y => y.CourseName),
					"ects" => x => x.OrderBy(y => y.Ects),
					"ects_desc" => x => x.OrderByDescending(y => y.Ects),
					_ => null
				};

			if (where == null && order == null)
				return _mapper.Map<IList<CourseRest>>(await GetAll());

			var courses = await _repo.GetAll(expression, orderBy);
			return _mapper.Map<IList<CourseRest>>(courses);
		}

		public async Task Insert(CreateCourseRest course)
		{
			var newCourse = _mapper.Map<Course>(course);
			newCourse.Id = Guid.NewGuid();
			await _repo.Insert(newCourse);
			await _repositoryWork.Save();
		}

		public async Task Update(Guid id, UpdateCourseRest newCourse)
		{
			var oldCourse = await _repo.Get(q => q.Id == id);
			if (oldCourse == null) throw new ArgumentException("Doesn't exist.");

			if (newCourse.Ects.HasValue) oldCourse.Ects = newCourse.Ects.Value;
			if (!string.IsNullOrEmpty(newCourse.CourseName)) oldCourse.CourseName = newCourse.CourseName;
			if (newCourse.TeacherId.HasValue) oldCourse.TeacherId = newCourse.TeacherId.Value;

			_repo.Update(oldCourse);
			await _repositoryWork.Save();
		}

		public async Task Delete(Guid id)
		{
			var course = await _repo.Get(x => x.Id == id);
			if (course == null) throw new ArgumentException("Doesn't exist.");
			await _repo.Delete(id);
			await _repositoryWork.Save();
		}
	}
}
