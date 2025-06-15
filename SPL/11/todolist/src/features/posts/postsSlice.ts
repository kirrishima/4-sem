import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { Post, NewPost } from './types';
import * as postsAPI from './postsAPI';

interface PostsState {
    posts: Post[];
    loading: boolean;
    error: string | null;
    nextClientId: number;
}

const initialState: PostsState = {
    posts: [],
    loading: false,
    error: null,
    nextClientId: 101,
};

export const fetchPostsThunk = createAsyncThunk('posts/fetchAll', postsAPI.fetchPosts);


export const createPostThunk = createAsyncThunk<Post, NewPost>(
    'posts/create',
    async (newPost, { getState }) => {
        const state = (getState() as { posts: PostsState }).posts;
        const id = state.nextClientId;

        await postsAPI.createPost(newPost);
        return { ...newPost, id };
    }
);


export const updatePostThunk = createAsyncThunk<Post, Post>(
    'posts/update',
    async (post, { getState }) => {
        if (post.id <= 100) {
            return postsAPI.updatePost(post);
        }

        return post;
    }
);


export const deletePostThunk = createAsyncThunk<number, number>(
    'posts/delete',
    async (id) => {
        if (id <= 100) {
            await postsAPI.deletePost(id);
        }
        return id;
    }
);

const postsSlice = createSlice({
    name: 'posts',
    initialState,
    reducers: {

    },
    extraReducers: builder => {
        builder
            .addCase(fetchPostsThunk.pending, state => {
                state.loading = true; state.error = null;
            })
            .addCase(fetchPostsThunk.fulfilled, (state, action) => {
                state.posts = action.payload;
                state.loading = false;
            })
            .addCase(fetchPostsThunk.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'Ошибка загрузки';
            })


            .addCase(createPostThunk.fulfilled, (state, action: PayloadAction<Post>) => {
                state.posts.unshift(action.payload);
                state.nextClientId += 1;
            })


            .addCase(updatePostThunk.fulfilled, (state, action: PayloadAction<Post>) => {
                const idx = state.posts.findIndex(p => p.id === action.payload.id);
                if (idx !== -1) {
                    state.posts[idx] = action.payload;
                }
            })


            .addCase(deletePostThunk.fulfilled, (state, action: PayloadAction<number>) => {
                state.posts = state.posts.filter(p => p.id !== action.payload);
            });
    },
});

export default postsSlice.reducer;
