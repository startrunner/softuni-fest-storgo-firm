//Помощни методи за заявки към сървъра
async function post(path, content, token){
    const res = await fetch(path, {
        method: 'POST',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(content)
    })
    
    return res;
}

async function get(path) {
    const res = await fetch(path, {
        method:'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    return res;
}

export {post, get}