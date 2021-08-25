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
	public class CourseService : IGenericService<CourseRest>
	{
		private readonly IRepositoryWork _repositoryWork;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<CourseDb> _repo;

		public CourseService(IRepositoryWork repositoryWork, IMapper mapper)
		{
			_repositoryWork = repositoryWork;
			_mapper = mapper;
			_repo = _repositoryWork.CourseDbRepository;
		}

		public async Task<CourseRest> GetById(Guid id)
		{
			var course = await _repo
				.Get(x => x.Id == id);
			return _mapper.Map<CourseRest>(course);
		}

		private async Task<IList<CourseDto>> GetAll()
		{
			var students = await _repo.GetAll();
			return _mapper.Map<IList<CourseDto>>(students);
		}

		public async Task<IList<CourseRest>> GetAll(string where, string order)
		{
			Func<IQueryable<CourseDb>, IOrderedQueryable<CourseDb>> orderBy = null;
			Expression<Func<CourseDb, bool>> expression = null;

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

		public async Task Insert(CourseRest course)
		{
			await _repo.Insert(_mapper.Map<CourseDb>(course));
			await _repositoryWork.Save();
		}

		public async Task Update(Guid id, CourseRest newCourse)
		{
			var oldCourse = await _repo.Get(q => q.Id == id);
			if (oldCourse == null) throw new ArgumentException("Doesn't exist.");

			_mapper.Map(newCourse, oldCourse);
			_repo.Update(oldCourse);
			await _repositoryWork.Save();
		}

		public async Task Delete(CourseRest course)
		{
			var courses = await _repo.GetAll(q =>
				q.CourseName == course.CourseName &&
				q.Ects == course.Ects
			);

			if (courses == null) throw new ArgumentException("Doesn't exist.");
			_repo.DeleteRange(courses);
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
