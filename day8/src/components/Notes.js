import NoteItem from "./NoteItem";

const Notes = props => (
	<div>
		{props.items.map(note =>
			{
				if (note.title === '' || note.text === '') return null;
				return (
					<NoteItem
						title={note.title}
						text={note.text}
					/>
				);
			}
		)}
	</div>
);

export default Notes;
