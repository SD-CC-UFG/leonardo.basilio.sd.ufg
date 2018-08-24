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

    (sexo, altura) = mensagem.split("\n")

    print("Sexo: " + sexo)
    print("Altura: " + altura)

    altura = float(altura)
    sexo = sexo.lower()

    if sexo == "m":
        mensagem = "%.2f" % (72.7*altura - 58)
    elif sexo == "f":
        mensagem = "%.2f" % (62.1*altura - 44.7)
    else:
        mensagem = "Sexo inválido"

    print("Peso ideal: " + mensagem)

    #============================

    buf = mensagem.encode("utf8")

    cliente.send(len(buf).to_bytes(4, "little"))
    cliente.send(buf)

    cliente.close()
