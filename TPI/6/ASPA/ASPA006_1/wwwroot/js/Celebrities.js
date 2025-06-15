document.addEventListener('DOMContentLoaded', () => {
    const style = document.createElement('style');
    style.textContent = `
        #celebrities-container {
          display: flex;
          flex-wrap: wrap;
          gap: 10px;
          padding: 10px;
        }
        #celebrities-container img {
          height: 200px;
          width: auto;
          border: 1px solid #ccc;
          padding: 5px;
          cursor: pointer;
        }
        #events-container {
          margin-top: 20px;
          padding: 10px;
        }
        .group-container {
          margin-bottom: 10px;
        }
        .event-line {
          margin-bottom: 5px;
        }
        .divider {
          border-top: 3px solid #ccc;
          margin: 10px 0;
        }
      `;
    document.head.appendChild(style);

    const container = document.createElement('div');
    container.id = 'celebrities-container';
    document.body.appendChild(container);

    const eventsContainer = document.createElement('div');
    eventsContainer.id = 'events-container';
    document.body.appendChild(eventsContainer);

    fetch('/api/Celebrities')
        .then(response => {
            if (!response.ok) {
                throw new Error(`Ошибка сети: ${response.status}`);
            }
            return response.json();
        })
        .then(celebrities => {
            celebrities.forEach(celebrity => {
                const img = document.createElement('img');
                img.src = `/api/Celebrities/photo/${celebrity.reqPhotoPath}`;
                img.alt = celebrity.fullName;
                img.dataset.id = celebrity.id;
                img.dataset.fullName = celebrity.fullName;

                img.addEventListener('click', () => {
                    fetch(`/api/Celebrities/Lifeevents/${celebrity.id}`)
                        .then(response => {
                            if (!response.ok) {
                                throw new Error(`Ошибка сети: ${response.status}`);
                            }
                            return response.json();
                        })
                        .then(lifeEvents => {
                            const groupContainer = document.createElement('div');
                            groupContainer.className = 'group-container';

                            lifeEvents.forEach(event => {
                                const eventLine = document.createElement('div');
                                eventLine.className = 'event-line';
                                const isoDateTime = new Date(event.date).toISOString().split('.')[0];
                                eventLine.textContent = `${celebrity.fullName} ${isoDateTime}  ${event.description}`;
                                groupContainer.appendChild(eventLine);
                            });

                            if (eventsContainer.childElementCount > 0) {
                                const divider = document.createElement('div');
                                divider.className = 'divider';
                                eventsContainer.prepend(divider);
                            }

                            eventsContainer.prepend(groupContainer);
                        })
                        .catch(error => console.error('Ошибка при загрузке событий:', error));
                });

                container.appendChild(img);
            });
        })
        .catch(error => console.error('Ошибка при загрузке данных:', error));
});
