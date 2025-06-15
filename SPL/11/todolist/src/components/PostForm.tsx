import { useState } from 'react';
import { useAppDispatch } from '../hooks';
import { createPostThunk } from '../features/posts/postsSlice';
import { NewPost } from '../features/posts/types';

const PostForm = () => {
  const dispatch = useAppDispatch();
  const [title, setTitle] = useState('');
  const [body,  setBody]  = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(createPostThunk({ title, body }));
    setTitle('');
    setBody('');
  };

  return (
    <form onSubmit={handleSubmit}>
      <h3>Добавить пост</h3>
      <input
        value={title}
        onChange={e => setTitle(e.target.value)}
        placeholder="Заголовок"
        required
      />
      <textarea
        value={body}
        onChange={e => setBody(e.target.value)}
        placeholder="Текст"
        required
      />
      <button type="submit">Добавить</button>
    </form>
  );
};

export default PostForm;
