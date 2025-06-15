// 1
const myPromise = new Promise<number>((resolve) => {
    setTimeout(() => {
        resolve(Math.random());
    }, 3000);
});

myPromise.then((num) => console.log("Случайное число:", num));

// 2
function createPromise(delay: number): Promise<number> {
    return new Promise<number>((resolve) => {
        setTimeout(() => {
            resolve(Math.random());
        }, delay);
    });
}

Promise.all([createPromise(3000), createPromise(5000), createPromise(8000)])
    .then((numbers) => console.log("Сгенерированные числа:", numbers))
    .catch((error) => console.error("Ошибка:", error));


// 3
let pr = new Promise((res, rej) => {
    rej('ku');
});


// 4
pr
    .then(() => console.log(1))
    .catch(() => console.log(2))
    .catch(() => console.log(3))
    .then(() => console.log(4))
    .then(() => console.log(5));


// 5
const promise21 = Promise.resolve(21);

promise21
    .then((result) => {
        console.log(result);
        return result;
    })
    .then((result) => {
        console.log(result * 2);
    });


// 6
async function runPromiseChain() {
    const result = await Promise.resolve(21);
    console.log(result);
    console.log(result * 2);
}

runPromiseChain();
