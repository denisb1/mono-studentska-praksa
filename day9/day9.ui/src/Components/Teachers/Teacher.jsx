import React, { Component } from "react";

class Teacher extends Component {
	teacher = this.props.teacher.courses.map((t) => {
		let { Id, FirstName, LastName, Department } = this.props.teacher;
		let { CourseName } = t;

		return (
			<tr key={Id + t.Id}>
				<td>{Id}</td>
				<td>{FirstName}</td>
				<td>{LastName}</td>
				<td>{Department}</td>
				<td>{CourseName}</td>
				<td>
					<div className="btn-group" role="group">
						<button className="btn btn-success">Edit</button>
						<button
							onClick={() => this.props.onDelete(Id)}
							className="btn btn-danger"
						>
							Delete
						</button>
					</div>
				</td>
			</tr>
		);
	});

	render() {
		return this.teacher;
	}
}

export default Teacher;
