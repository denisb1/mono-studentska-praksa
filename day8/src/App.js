import NewNote from "./components/NewNote";
import Notes from "./components/Notes"
import {useState} from "react";
import './App.css';

const DUMMY_NOTES = [
	{
		title: 'Lorem ipsum',
		text: 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.'
	}
];

const App = () => {
	const [notes, setNotes] = useState(DUMMY_NOTES);
	const addNoteHandler = note => {
		setNotes(prevNotes => {
			return [...prevNotes, note];
		});
	};

	return (
		<div className="App">
			<header className="App-header">
				<div className='main'>
					<NewNote onAddNote={addNoteHandler}/>
					<Notes items={notes}/>
				</div>
			</header>
		</div>
	);
};

export default App;
