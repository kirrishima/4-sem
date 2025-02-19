var __spreadArray = (this && this.__spreadArray) || function (to, from, pack) {
    if (pack || arguments.length === 2) for (var i = 0, l = from.length, ar; i < l; i++) {
        if (ar || !(i in from)) {
            if (!ar) ar = Array.prototype.slice.call(from, 0, i);
            ar[i] = from[i];
        }
    }
    return to.concat(ar || Array.prototype.slice.call(from));
};
function createPhoneNumber(numbers) {
    var part1 = numbers.slice(0, 3).join('');
    var part2 = numbers.slice(3, 6).join('');
    var part3 = numbers.slice(6).join('');
    return "(".concat(part1, ") ").concat(part2, "-").concat(part3);
}
console.log(createPhoneNumber([1, 2, 3, 4, 5, 6, 7, 8, 9, 0]));
// задание 2
var Challenge = /** @class */ (function () {
    function Challenge() {
    }
    Challenge.solution = function (number) {
        if (number < 0) {
            return 0;
        }
        var sum = 0;
        for (var i = 1; i < number; i++) {
            if (i % 3 === 0 || i % 5 === 0) {
                sum += i;
            }
        }
        return sum;
    };
    return Challenge;
}());
console.log(Challenge.solution(10));
// задание 3
function rotateArray(nums, k) {
    var len = nums.length;
    if (len === 0)
        return nums;
    k = k % len;
    return nums.slice(-k).concat(nums.slice(0, len - k));
}
console.log(rotateArray([1, 2, 3, 4, 5, 6, 7], 3));
// задание 4
function findMedianSortedArrays(nums1, nums2) {
    var merged = __spreadArray(__spreadArray([], nums1, true), nums2, true).sort(function (a, b) { return a - b; });
    var mid = Math.floor(merged.length / 2);
    if (merged.length % 2 !== 0) {
        return merged[mid];
    }
    else {
        return (merged[mid - 1] + merged[mid]) / 2;
    }
}
console.log(findMedianSortedArrays([1, 3], [2]));
console.log(findMedianSortedArrays([1, 2], [3, 4]));
