function createPhoneNumber(numbers) {
    let part1 = numbers.slice(0, 3).join('');
    let part2 = numbers.slice(3, 6).join('');
    let part3 = numbers.slice(6).join('');
    return `(${part1}) ${part2}-${part3}`;
}
console.log(createPhoneNumber([1, 2, 3, 4, 5, 6, 7, 8, 9, 0]));
// задание 2
class Challenge {
    static solution(number) {
        if (number < 0) {
            return 0;
        }
        let sum = 0;
        for (let i = 1; i < number; i++) {
            if (i % 3 === 0 || i % 5 === 0) {
                sum += i;
            }
        }
        return sum;
    }
}
console.log(Challenge.solution(10));
// задание 3
function rotateArray(nums, k) {
    const len = nums.length;
    if (len === 0)
        return nums;
    k = k % len;
    return nums.slice(-k).concat(nums.slice(0, len - k));
}
console.log(rotateArray([1, 2, 3, 4, 5, 6, 7], 3));
// задание 4
function findMedianSortedArrays(nums1, nums2) {
    const merged = [...nums1, ...nums2].sort((a, b) => a - b);
    const mid = Math.floor(merged.length / 2);
    if (merged.length % 2 !== 0) {
        return merged[mid];
    }
    else {
        return (merged[mid - 1] + merged[mid]) / 2;
    }
}
console.log(findMedianSortedArrays([1, 3], [2]));
console.log(findMedianSortedArrays([1, 2], [3, 4]));
