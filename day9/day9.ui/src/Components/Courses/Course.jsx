import React, { Component } from "react";

class Course extends Component {
	render() {
		let { Id, CourseName, Ects } = this.props.course;

		return (
			<tr key={Id}>
				<td>{Id}</td>
				<td>{CourseName}</td>
				<td>{Ects}</td>
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
	}
}

export default Course;
