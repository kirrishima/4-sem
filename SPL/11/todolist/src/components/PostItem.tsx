import { useState } from 'react';
import { Post } from '../features/posts/types';
import { useAppDispatch } from '../hooks';
import {
  updatePostThunk,
  deletePostThunk,
} from '../features/posts/postsSlice';

interface Props {
  post: Post;
}

const PostItem = ({ post }: Props) => {
  const dispatch = useAppDispatch();
  const [isEditing, setEditing] = useState(false);
  const [title, setTitle]       = useState(post.title);
  const [body,  setBody]        = useState(post.body);

  const handleSave = () => {
    dispatch(updatePostThunk({ ...post, title, body }));
    setEditing(false);
  };

  const handleDelete = () => {
    dispatch(deletePostThunk(post.id));
  };

  return (
    <div style={{ border: '1px solid #ccc', padding: 10, marginTop: 10 }}>
      {isEditing ? (
        <>
          <input   value={title} onChange={e => setTitle(e.target.value)} />
          <textarea value={body}  onChange={e => setBody(e.target.value)} />
          <button onClick={handleSave}>Сохранить</button>
          <button onClick={() => setEditing(false)}>Отмена</button>
        </>
      ) : (
        <>
          <h4>{post.title}</h4>
          <p>{post.body}</p>
          <button onClick={() => setEditing(true)}>Редактировать</button>
        </>
      )}
      <button onClick={handleDelete}>Удалить</button>
    </div>
  );
};

export default PostItem;
