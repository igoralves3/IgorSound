# IgorSound

Bem-vindo ao repositório **IgorSound**! Este projeto é uma biblioteca de animação com som desenvolvida como trabalho de faculdade para a disciplina de Animação Computadorizada. O projeto foi realizado pelos alunos:

- Cindi Saicosque
- Igor Alves
- Guilherme

## Índice

- [Características](#características)
- [Instalação](#instalação)
- [Uso](#uso)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Características

- O objeto dança e muda de cor conforme a frequência da música
- É possível aumentar e diminuir o volume do pitch pressionando as teclas para cima e para baixo, respectivamente

## Instalação

Para instalar o IgorSound, você pode usar o npm ou baixar os arquivos diretamente.

### Usando npm

```bash
npm install igoranime
```

### Ou baixar manualmente

1. Acesse a [página de releases](https://github.com/igoralves3/IgorAnimation/releases).
2. Baixe a versão mais recente e extraia os arquivos.

## Uso

Após a instalação, você pode começar a usar a biblioteca em seu projeto. Aqui está um exemplo básico de como utilizar:

```javascript
import { animate } from 'igoranime';

// Exemplo de animação
animate('.element', {
  opacity: 1,
  transform: 'translateY(0)',
}, {
  duration: 1000,
  easing: 'ease-in-out'
});
```

Para mais exemplos e detalhes, consulte a [documentação completa](link para a documentação).

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests. Para contribuir, siga estas etapas:

1. Fork o projeto
2. Crie sua branch (`git checkout -b feature/nova-funcionalidade`)
3. Commit suas alterações (`git commit -m 'Adicionando nova funcionalidade'`)
4. Push para a branch (`git push origin feature/nova-funcionalidade`)
5. Abra um Pull Request

## Assets externos
https://assetstore.unity.com/packages/tools/modeling/deform-148425
https://assetstore.unity.com/packages/tools/audio/simplespectrum-free-audio-spectrum-generator-webgl-85294

## Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE).
