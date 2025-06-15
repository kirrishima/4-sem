import axios from 'axios';
import { Post, NewPost } from './types';

const API_URL = 'https://jsonplaceholder.typicode.com/posts';

export const fetchPosts = async (): Promise<Post[]> => {
    const response = await axios.get<Post[]>(API_URL);
    return response.data;
};

export const createPost = async (newPost: NewPost): Promise<Post> => {
    const response = await axios.post<Post>(API_URL, newPost);
    return response.data;
};

export const updatePost = async (post: Post): Promise<Post> => {
    const response = await axios.put<Post>(`${API_URL}/${post.id}`, post);
    return response.data;
};

export const deletePost = async (id: number): Promise<number> => {
    await axios.delete(`${API_URL}/${id}`);
    return id;
};
