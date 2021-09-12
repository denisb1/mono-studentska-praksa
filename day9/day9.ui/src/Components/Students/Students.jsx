import "bootstrap/dist/css/bootstrap.css";
import "../Common.css";

import React, { Component } from "react";
import axios from "axios";

import Api from "../../Utils/Api";
import Student from "./Student";

class Students extends Component {
	state = {
		students: {
			Id: "",
			FirstName: "",
			LastName: "",
			Age: "",
			Year: "",
			enrollments: {
				course: {
					Id: "",
					CourseName: ""
				}
			}
		},
		isLoaded: false
	};

	async componentDidMount() {
		this.setState({ isLoaded: false });
		await this.getStudents();
	}

	getStudents = async () => {
		this.setState({ isLoaded: false });
		const { data: students } = await axios.get(Api.studentEndpoint());
		this.setState({ students: students, isLoaded: true });
	};

	renderStudents = () => {
		if (this.state.isLoaded)
			return (
				<React.Fragment>
					<table className="table table-striped">
						<thead>
							<tr>
								<th scope="col">Id</th>
								<th scope="col">First name</th>
								<th scope="col">Last name</th>
								<th scope="col">Age</th>
								<th scope="col">Year</th>
								<th scope="col">Course</th>
								<th scope="col">Actions</th>
							</tr>
						</thead>
						<tbody>
							{this.state.students.map((student) => (
								<Student key={student.Id} student={student} />
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
		else return <h3>Loading students ...</h3>;
	};

	render() {
		return this.renderStudents();
	}
}

export default Students;
