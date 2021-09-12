import "bootstrap/dist/css/bootstrap.css";
import "./App.css";

import React from "react";

import Teachers from "./Components/Teachers/Teachers";
import Courses from "./Components/Courses/Courses";
import Students from "./Components/Students/Students";

const App = () => (
	<React.Fragment>
		<div className="App container">
			<Courses />
			<Students />
			<Teachers />
		</div>
	</React.Fragment>
);

export default App;
