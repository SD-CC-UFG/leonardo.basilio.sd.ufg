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

    (n1, n2, n3) = mensagem.split("\n")

    print("N1: " + n1)
    print("N2: " + n2)
    print("N3: " + n3)

    n1 = float(n1)
    n2 = float(n2)
    n3 = float(n3)

    m = (n1+n2)/2

    if (m >= 7.0) or ((m+n3)/2 >= 5.0):
        mensagem = "Aprovado"
    else:
        mensagem = "Reprovado"

    print("Situação: " + mensagem)

    #============================

    buf = mensagem.encode("utf8")

    cliente.send(len(buf).to_bytes(4, "little"))
    cliente.send(buf)

    cliente.close()
