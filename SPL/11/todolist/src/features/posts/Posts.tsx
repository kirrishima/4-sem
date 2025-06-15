import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../../hooks';
import {
  fetchPostsThunk,
} from './postsSlice';
import PostForm from '../../components/PostForm';
import PostItem from '../../components/PostItem';

const Posts = () => {
  const dispatch = useAppDispatch();
  const { posts, loading, error } = useAppSelector(s => s.posts);

  useEffect(() => {
    dispatch(fetchPostsThunk());
  }, [dispatch]);

  if (loading) return <p>Загрузка...</p>;
  if (error)   return <p>Ошибка: {error}</p>;

  return (
    <div>
      <h2>Список постов</h2>
      <PostForm />
      {posts.map(post => (
        <PostItem key={post.id} post={post} />
      ))}
    </div>
  );
};

export default Posts;
