class Api {
	static ip = "localhost";
	static courseEndpoint = () => "https://" + this.ip + ":5001/api/Course";
	static studentEndpoint = () => "https://" + this.ip + ":5001/api/Student";
	static teacherEndpoint = () => "https://" + this.ip + ":5001/api/Teacher";
}

export default Api;
