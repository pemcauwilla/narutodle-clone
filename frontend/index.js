const API_BASE_URL = 'http://localhost:5256';

const input = document.querySelector('.char-input');
const form = document.querySelector('.input-form');
const charListDiv = document.querySelector('.char-list');
const guessesContainer = document.querySelector('.guesses-container');
const charTemplate = document.querySelector('.character-block');
const guessTemplate = document.querySelector('.guess-template');

let allNinjaNames = [];

async function loadNames() {
    try {
        const response = await fetch(`${API_BASE_URL}/api/game/names`); 
        allNinjaNames = await response.json();
    } catch (error) {
        console.error("Failed to load names:", error);
    }
}
loadNames();

let debounceTimer;

input.addEventListener('input', (e) => {
    clearTimeout(debounceTimer);
    const query = e.target.value.toLowerCase().trim();
    
    if (query.length === 0) {
        charListDiv.classList.add('hidden');
        return;
    }

    debounceTimer = setTimeout(() => {
        const matches = allNinjaNames.filter(ninja => ninja.name.toLowerCase().includes(query));
        
        charListDiv.innerHTML = '';
        
        if (matches.length > 0) {
            matches.slice(0, 10).forEach(ninja => { 
                const clone = charTemplate.content.cloneNode(true);
                
                const p = clone.querySelector('p');
                p.textContent = ninja.name; 
                
                const img = clone.querySelector('img');
                img.src = ninja.imageUrl; 

                const element = clone.querySelector('.char-list-element');
                element.addEventListener('click', () => {
                    input.value = ninja.name; 
                    charListDiv.classList.add('hidden');
                    submitGuess(ninja.name);  
                });

                charListDiv.appendChild(clone);
            });
            charListDiv.classList.remove('hidden');
        } else {
            charListDiv.classList.add('hidden');
        }
    }, 300);
});

document.addEventListener('click', (e) => {
    if (!form.contains(e.target) && !charListDiv.contains(e.target)) {
        charListDiv.classList.add('hidden');
    }
});

form.addEventListener('submit', (e) => {
    e.preventDefault();
    const guessName = input.value.trim();
    if (guessName) {
        charListDiv.classList.add('hidden');
        submitGuess(guessName);
    }
});

async function submitGuess(name) {
    try {
        const response = await fetch(`${API_BASE_URL}/api/game/guess/${encodeURIComponent(name)}`, {
            method: 'POST'
        });

        if (!response.ok) {
            alert("Character not found!");
            return;
        }

        const result = await response.json();
        renderGuess(result);
        
        input.value = ''; 
        
        if (result.isVictory) {
            setTimeout(() => alert("You found the Daily Ninja!"), 500);
        }
        
    } catch (error) {
        console.error("Error submitting guess:", error);
    }
}

function renderGuess(data) {
    guessesContainer.classList.remove('hidden');

    const clone = guessTemplate.content.cloneNode(true);
    const guessDiv = clone.querySelector('.guess');
    const ps = guessDiv.querySelectorAll('p'); 

    const getStatusClass = (status) => {
        const statusMap = {
            0: 'status-incorrect',
            1: 'status-partial',
            2: 'status-correct',
            3: 'status-earlier',
            4: 'status-later'
        };
        return statusMap[status] || 'status-incorrect';
    };


    guessDiv.querySelector('img').src = data.imageUrl;


    ps[0].textContent = data.gender.value; 
    ps[0].classList.add(getStatusClass(data.gender.status));


    ps[1].textContent = data.affiliations.value.length > 0 ? data.affiliations.value.join(', ') : "None"; 
    ps[1].classList.add(getStatusClass(data.affiliations.status));


    ps[2].textContent = data.jutsuTypes.value.length > 0 ? data.jutsuTypes.value.join(', ') : "None";
    ps[2].classList.add(getStatusClass(data.jutsuTypes.status));


    ps[3].textContent = data.kekkeiGenkais.value.length > 0 ? data.kekkeiGenkais.value.join(', ') : "None";
    ps[3].classList.add(getStatusClass(data.kekkeiGenkais.status));


    const natureContainer = guessDiv.querySelector('.nature-types');
    natureContainer.classList.add(getStatusClass(data.natureTypes.status));
    
    if (data.natureTypes.value.length === 0) {
        natureContainer.innerHTML = '<span style="color:white;">None</span>';
    } else {

        data.natureTypes.value.forEach(natureStr => {
            const img = document.createElement('img');
            img.src = `assets/natures/${natureStr}.png`; 
            natureContainer.appendChild(img);
        });
    }
    ps[4].textContent = data.classifications && data.classifications.value.length > 0 ? data.classifications.value.join(', ') : "None";
    ps[4].classList.add(getStatusClass(data.classifications ? data.classifications.status : 0));

    ps[5].textContent = data.debutArc.value;
    ps[5].classList.add(getStatusClass(data.debutArc.status));

    guessesContainer.insertBefore(clone, guessesContainer.children[1]);
}

const clone = guessTemplate.content.cloneNode(true);
    const guessDiv = clone.querySelector('.guess');
    const ps = guessDiv.querySelectorAll('p'); 

    guessDiv.classList.add('animate-row');

    const boxes = guessDiv.children;
    for (let i = 0; i < boxes.length; i++) {
     
        boxes[i].style.animationDelay = `${i * 0.15}s`; 
    }
    
