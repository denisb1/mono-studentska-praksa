import {useState} from "react";
import './NoteForm.css'

const NoteForm = props => {
	const [userInput, setUserInput] = useState({
		enteredTitle: '',
		enteredText: ''
	});

	const titleChangeHandler = event => {
		setUserInput(prevState => {
			return { ...prevState, enteredTitle: event.target.value };
		});
	};

	const textChangeHandler = event => {
		setUserInput(prevState => {
			return { ...prevState, enteredText: event.target.value };
		});
	};

	const submitHandler = event => {
		event.preventDefault();
		const noteData = {
			title: userInput.enteredTitle,
			text: userInput.enteredText
		}

		props.onSaveNoteData(noteData);
		setUserInput(() => {
			return { enteredTitle: '', enteredText: '' };
		});
	}

	return (
		<form onSubmit={submitHandler}>
			<div>
				<div>
					<input
						placeholder='Title'
						value={userInput.enteredTitle}
						type='text'
						className='input-title'
						onChange={titleChangeHandler}
					/>
				</div>
				<div>
					<textarea
						placeholder='Text'
						value={userInput.enteredText}
						className='input-text'
						onChange={textChangeHandler}
					/>
				</div>
			</div>
			<div>
				<button
					type='submit'
					className='button-submit'>
					Add note
				</button>
			</div>
		</form>
	);
};

export default NoteForm;
