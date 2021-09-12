import React, { Component } from "react";

class Student extends Component {
	student = this.props.student.enrollments.map((s) => {
		let { Id, FirstName, LastName, Age, Year } = this.props.student;
		let { courseName } = s.course;

		return (
			<tr key={Id + s.course.id}>
				<td>{Id}</td>
				<td>{FirstName}</td>
				<td>{LastName}</td>
				<td>{Age}</td>
				<td>{Year}</td>
				<td>{courseName}</td>
				<td>
					<div className="btn-group" role="group">
						<button className="btn btn-success">Edit</button>
						<button className="btn btn-danger">Delete</button>
					</div>
				</td>
			</tr>
		);
	});

	render() {
		return this.student;
	}
}

export default Student;
