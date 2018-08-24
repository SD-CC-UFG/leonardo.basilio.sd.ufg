import socket

servidor = socket.socket()

servidor.bind(("0.0.0.0", 8888))

servidor.listen()

while 1:

    print("Aguardando conexão...")

    (cliente, cliente_end) = servidor.accept()

    print("Conexão recebida de: " + cliente_end[0])

    #============================

    buf = bytearray()

    while len(buf) < 4:
        buf += cliente.recv(4)

    mensagem_tam = int.from_bytes(buf[0:4], "little")

    buf = buf[4:]
    
    while len(buf) < mensagem_tam:
        buf += cliente.recv(mensagem_tam - len(buf))

    mensagem = buf.decode("utf8")

    #============================

    idade = mensagem

    print("Idade: " + idade)

    idade = int(idade)

    if idade < 5:
        mensagem = "-"
    elif idade <= 7:
        mensagem = "Infantil A"
    elif idade <= 10:
        mensagem = "Infantil B"
    elif idade <= 13:
        mensagem = "Juvenil A"
    elif idade <= 17:
        mensagem = "Juvenil B"
    else:
        mensagem = "Adulto"

    print("Categoria: " + mensagem)

    #============================

    buf = mensagem.encode("utf8")

    cliente.send(len(buf).to_bytes(4, "little"))
    cliente.send(buf)

    cliente.close()
