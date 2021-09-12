import "bootstrap/dist/css/bootstrap.css";
import "../Common.css";

import React, { Component } from "react";
import axios from "axios";

import Api from "../../Utils/Api";
import Teacher from "./Teacher";

class Teachers extends Component {
	state = {
		teachers: {
			Id: "",
			FirstName: "",
			LastName: "",
			Courses: {
				Id: "",
				CourseName: ""
			}
		},
		isLoaded: false
	};

	async componentDidMount() {
		this.setState({ isLoaded: false });
		await this.getTeachers();
	}

	getTeachers = async () => {
		this.setState({ isLoaded: false });
		const { data: teachers } = await axios.get(Api.teacherEndpoint());
		this.setState({ teachers: teachers, isLoaded: true });
	};

	handleDelete = async (id) => {
		await axios.delete(Api.teacherEndpoint() + "/" + id);
		await this.getTeachers();
	};

	renderTeachers = () => {
		if (this.state.isLoaded)
			return (
				<React.Fragment>
					<table className="table table-striped">
						<thead>
							<tr>
								<th scope="col">Id</th>
								<th scope="col">First name</th>
								<th scope="col">Last name</th>
								<th scope="col">Department</th>
								<th scope="col">Course</th>
								<th scope="col">Actions</th>
							</tr>
						</thead>
						<tbody>
							{this.state.teachers.map((teacher) => (
								<Teacher
									onDelete={this.handleDelete}
									key={teacher.Id}
									teacher={teacher}
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
		else return <h3>Loading teachers ...</h3>;
	};

	render() {
		return this.renderTeachers();
	}
}

export default Teachers;
