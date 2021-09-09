import './NoteItem.css'

const NoteItem = props => (
	<div className='note-item'>
		<h2 className='note-item-title'>{props.title}</h2>
		<p className='note-item-text'>{props.text}</p>
	</div>
);

export default NoteItem;
