// 1 задание

abstract class BaseUser {
    protected id: number;
    protected name: string;
 
    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
    }

    abstract getRole(): string;

    abstract getPermissions(): string[];

    public getName(): string {
        return this.name;
    }
}


class Guest extends BaseUser {
    getRole(): string {
        return "Guest";
    }

    getPermissions(): string[] {
        return ["Просмотр"];
    }
}


class User extends BaseUser {
    getRole(): string {
        return "User";
    }

    getPermissions(): string[] {
        return ["Просмотр", "Редактирование"];
    }
}


class Admin extends BaseUser {
    getRole(): string {
        return "Admin";
    }

    getPermissions(): string[] {
        return ["Просмотр", "Удаление", "Управление"];
    }
}


const guest = new Guest(1, "Хто");
console.log(guest.getName());
console.log(guest.getPermissions());

const user = new User(2, "Антон");
console.log(user.getName());
console.log(user.getPermissions());

const admin = new Admin(3, "Райан Гослинг");
console.log(admin.getName());
console.log(admin.getPermissions());


// 2 Задание
interface IReport {
    title: string;
    content: string;
    generate(): string | object;
}


class HTMLReport implements IReport {
    constructor(public title: string, public content: string) { }

    generate(): string {
        return `<h1>${this.title}</h1><p>${this.content}</p>`;
    }
}


class JSONReport implements IReport {
    constructor(public title: string, public content: string) { }

    generate(): object {
        return {
            title: this.title,
            content: this.content,
        };
    }
}


const report1 = new HTMLReport("Отчет 1", "Содержание отчета");
console.log(report1.generate());

const report2 = new JSONReport("Отчет 2", "Содержание отчета");
console.log(report2.generate());

const reports: IReport[] = [report1, report2];
reports.forEach((r) => {
    console.log(r.generate());
});


{
    // 3 задание
    class Cache<T> {
        store: Map<string, { value: T; expiry: number }> = new Map();

        add(key: string, value: T, ttl: number): void {
            const expiry = Date.now() + ttl;
            this.store.set(key, { value, expiry });
        }

        get(key: string): T | null {
            const item = this.store.get(key);
            if (!item) {
                return null;
            }

            if (Date.now() > item.expiry) {
                this.store.delete(key);
                return null;
            }
            return item.value;
        }

        clearExpired(): void {
            const now = Date.now();
            for (const [key, { expiry }] of this.store.entries()) {
                if (now > expiry) {
                    this.store.delete(key);
                }
            }
        }
    }

    const cache = new Cache<number>();
    cache.add("price", 100, 5000);
    console.log(cache.get("price"));
    setTimeout(() => console.log(cache.get("price")), 6000);
}


// 4 задание
function createInstance<T>(cls: new (...args: any[]) => T, ...args: any[]): T {
    return new cls(...args);
}


const newGuest = createInstance(Guest, 10, "Гость Иван");
console.log(newGuest.getName());
console.log(newGuest.getRole());
console.log(newGuest.getPermissions());


const newHtmlReport = createInstance(HTMLReport, "Новый отчет", "Текст отчета");
console.log(newHtmlReport.generate());


// 5 задание
enum LogLevel {
    INFO = 'INFO',
    WARNING = 'WARNING',
    ERROR = 'ERROR',
}


type LogEntry = [Date, LogLevel, string];


function logEvent([timestamp, level, message]: LogEntry): void {
    console.log(`[${timestamp.toISOString()}] [${level}]: ${message}`);
}


logEvent([new Date(), LogLevel.INFO, 'Система запущена']);


//   6 задание
enum HttpStatus {
    OK = 200,
    BAD_REQUEST = 400,
    UNAUTHORIZED = 401,
    NOT_FOUND = 404,
    INTERNAL_SERVER_ERROR = 500,
}


type ApiResponse<T> = [status: HttpStatus, data: T | null, error?: string];


function success<T>(data: T): ApiResponse<T> {
    return [HttpStatus.OK, data];
}

function error(message: string, status: HttpStatus): ApiResponse<null> {
    return [status, null, message];
}


const res1 = success({ user: 'Андрей' });
console.log(res1);

const res2 = error('Не найдено', HttpStatus.NOT_FOUND);
console.log(res2);
