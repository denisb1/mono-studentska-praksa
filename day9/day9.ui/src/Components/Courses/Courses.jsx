import "bootstrap/dist/css/bootstrap.css";
import "../Common.css";

import React, { Component } from "react";
import axios from "axios";

import Api from "../../Utils/Api";
import Course from "./Course";

class Courses extends Component {
	state = {
		courses: {
			Id: "",
			CourseName: "",
			Ects: ""
		},
		isLoaded: false
	};

	async componentDidMount() {
		this.setState({ isLoaded: false });
		await this.getCourses();
	}

	getCourses = async () => {
		this.setState({ isLoaded: false });
		const { data: courses } = await axios.get(Api.courseEndpoint());
		this.setState({ courses: courses, isLoaded: true });
	};

	handleDelete = async (id) => {
		await axios.delete(Api.courseEndpoint() + "/" + id);
		await this.getCourses();
	};

	renderCourses = () => {
		if (this.state.isLoaded)
			return (
				<React.Fragment>
					<table className="table table-striped">
						<thead>
							<tr>
								<th scope="col">Id</th>
								<th scope="col">Course name</th>
								<th scope="col">Ects</th>
								<th scope="col">Actions</th>
							</tr>
						</thead>
						<tbody>
							{this.state.courses.map((course) => (
								<Course
									onDelete={this.handleDelete}
									key={course.Id}
									course={course}
								/>
							))}
						</tbody>
					</table>
					<table>
						<tbody>
							<tr>
								<td>
									<button className="btn btn-primary m-2 align-left">
										Add
									</button>
								</td>
							</tr>
						</tbody>
					</table>
				</React.Fragment>
			);
		else return <h3>Loading courses ...</h3>;
	};

	render() {
		return this.renderCourses();
	}
}

export default Courses;
