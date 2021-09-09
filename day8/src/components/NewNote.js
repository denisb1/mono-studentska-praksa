import NoteForm from "./NoteForm";

const NewNote = props => {
	const saveNoteDataHandler = enteredNoteData => {
		props.onAddNote(enteredNoteData);
	};

	return (
		<div>
			<NoteForm onSaveNoteData={saveNoteDataHandler}/>
		</div>
	);
}

export default NewNote;
