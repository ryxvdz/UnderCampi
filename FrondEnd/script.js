document.addEventListener('DOMContentLoaded', () => {
    // *** ATENÇÃO: SUBSTITUA A PORTA ABAIXO PELA PORTA DA SUA API (ex: 5182, 7001, etc.) ***
    // Verifique no terminal onde seu backend C# está rodando (dotnet run)
    const API_BASE_URL = 'http://localhost:5182/api/NoticiasShows'; 

    const showsListDiv = document.getElementById('shows-list');
    const loadingMessage = document.getElementById('loading-message');
    const errorMessage = document.getElementById('error-message');

    // Função para formatar a data
    function formatarDataHora(dataString) {
        const options = {
            year: 'numeric', month: 'long', day: 'numeric',
            hour: '2-digit', minute: '2-digit'
        };
        return new Date(dataString).toLocaleDateString('pt-BR', options);
    }

    // Função para buscar e exibir os shows
    async function fetchShows() {
        loadingMessage.style.display = 'block'; // Mostra mensagem de carregamento
        errorMessage.style.display = 'none';    // Esconde mensagem de erro
        showsListDiv.innerHTML = '';            // Limpa shows anteriores

        try {
            const response = await fetch(API_BASE_URL);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const shows = await response.json();

            loadingMessage.style.display = 'none'; // Esconde mensagem de carregamento

            if (shows.length === 0) {
                showsListDiv.innerHTML = '<div class="message">Nenhum show cadastrado ainda.</div>';
            } else {
                shows.forEach(show => {
                    const card = document.createElement('div');
                    card.className = 'show-card';

                    const imageUrl = show.urlImagemCapa || 'https://via.placeholder.com/300x200.png?text=Imagem+do+Show';

                    card.innerHTML = `
                        <img src="${imageUrl}" alt="${show.titulo || 'Imagem do Show'}">
                        <div class="show-card-content">
                            <h3>${show.titulo}</h3>
                            <p>${show.descricao.substring(0, 150)}...</p>
                            <p><span class="info-label">Quando:</span> ${formatarDataHora(show.dataShow)}</p>
                            <p><span class="info-label">Onde:</span> ${show.local}</p>
                            ${show.bandas ? `<p><span class="info-label">Bandas:</span> ${show.bandas}</p>` : ''}
                            ${show.precoIngresso ? `<p class="price">Ingressos a partir de: R$ ${show.precoIngresso.toFixed(2).replace('.', ',')}</p>` : ''}
                            ${show.linkIngressos ? `<a href="${show.linkIngressos}" target="_blank" class="link-ingressos">Comprar Ingressos</a>` : ''}
                        </div>
                    `;
                    showsListDiv.appendChild(card);
                });
            }
        } catch (error) {
            console.error('Erro ao buscar shows:', error);
            loadingMessage.style.display = 'none';
            errorMessage.style.display = 'block'; 
        }
    }

    fetchShows();
});