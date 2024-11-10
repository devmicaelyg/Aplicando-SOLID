## O que é o SOLID?

São princípios fundamentais para escrever códigos mais limpos, legíveis e de melhor manutenção

**Principais Benefícios:**
- Maior testabilidade
- Menor acoplamento
- Melhor organização do código

Acrônimo para: 
	**S**ingle Responsibility Principle
	**O**pen/Closed Principle
	**L**inkov’s Substution Principle
	**I**nterface Segregation Principle
	**D**ependency Inversion Principle

#### Single Responsibility Principle

A Frase que representa o principio: “There should never be more than one repasso for a class to change” (Não deve existir mais uma razão para uma classe ser alterada)

Ou seja, cada classe deve ter apenas **UMA** responsabilidade

Benefícios: 
- Menor acoplamento
    - Classes com menor acoplamento tendem a ter menos dependências
- Testabilidade
    - Quanto menos responsabilidade, menor são os casos de teste para escrever e mais simplicidade para fazê-lo
- Manutenibilidade
    - Classes menores são mais fáceis de se ler e manter

##### Exemplo: 
**Contexto:** Um serviço da camada de aplicação responsável pela implementação de casos de uso relacionado a Pessoa.

O caso de uso foco será o de cadastro de Pessoa. Para isso, existe uma serie de operações associadas que não estão necessariamente ligadas ao caso de uso em si. São elas:

- Mapeamento para uma entidade do domínio
- Persistência de dados através de um repositório
- Envio para uma fila

Observe o seguinte código:
```
public class PersonService
{
	public async void Add(PersonDto dto) 
	{
		//Mapeamento para uma entidade do dominio
	    var person = new Person(dto.Name, dto.Document, dto.BirthDate);

	    //Implementa a persistencia com o banco de dados
		DataContext context;
        bool added = await context.Person.AddAsync(person);

        if (added) 
        {
	        //Conexão com a fila 
            //Código de conexão com a fila     
        }
    }
}
```

Como pode ser observado, o método de adicionar tem várias funções: mapeamento, conexão com o contexto para adicionar ao banco de dados a entidade, controle se foi salvo ou não, configuração de conexão com a fila ao qual será enviada e o envio.

Isso fere o principio, pois o método de adicionar uma pessoa vai está fazendo muito mais coisa do que adicionar de fato uma pessoa.

Para resolver isso, separaremos em métodos, interfaces, classes.. onde cada um vai ter o seu objetivo a cumprir.

Adicionando o principio a nossa classe serviço ficaria do seguinte jeito:

```
    public class PersonService
    {
        private readonly PersonRepository _personRepository;
        private readonly ServiceBusService _serviceBusService;

        public PersonService(PersonRepository personRepository, ServiceBusService serviceBusService)
        {
            _personRepository = personRepository;
            _serviceBusService = serviceBusService;
            _erpSyncService = erpSyncService;
        }
        
        public void Add(PersonDto dto) 
        {
            var person = new Person(dto.Name, dto.Document, dto.BirthDate);

            _personRepository.Add(person);

            _serviceBusService.Publish("nome_fila", person);
        }
    }
```

Podemos observar duas injeções de dependência do serviço:
- serviceBusService: que ficará responsável pela fila
- personRepository: que ficará responsável pela comunicação com o banco de dados

Dessa forma o nosso método Add que antes se preocupava com 3 coisas, passa a se preocupar com o mapeamento da entidade Person e o envio para as classes que estão responsáveis pelo resto. 

Depois de aplicado o principio, cada classe desempenha o seu papel e suas responsabilidades ficam totalmente separadas e únicas.

#### Open/Closed Principle

Frase que representa o principio: “A module should be open for extensions but closed for modifications”

Isso significa que ela deve ser extensível mas que, em caso de novas funcionalidades relacionadas, você não deve alterar o código existente

*OBS: É necessário o entendimento de como se usa Interfaces e Composições para poder entender melhor a aplicação desse principio.* 

##### Exemplo: 
**Contexto:** O desenvolvimento de um processamento de pagamentos de uma hamburgueria

Em um primeiro momento a hamburgueria aceita somente cartão de crédito e débito e cada forma de pagamento tem o seu processo individual de processamento, ou seja, cada um processa de um jeito diferente. 

Portanto, se o pagamento for Débito o processamento é A e se o pagamento for Crédito o processamento é B. Se isso ou se aquilo nós já pensamos automaticamente em ifs, estruturando desse jeito: 
```
public class PaymentService 
{
	public void Process(OrderPaymentInfo paymentInfo)
	{
		if(paymentInfo.Type == PaymentType.Credit)
		{
			//Processamento B
		} else if(paymentInfo.Type == PaymentType.Debit)
		{
			//Processamento A
		}
	}
}
```

Porém, se surgir em algum momento uma nova forma de pagamento teríamos que adicionar mais um else if, ferindo completamente esse principio. 

Ao deixar estruturado desse jeito, o código tende sempre a ser modificado diminuindo a sua estabilidade e aumentando cada vez mais o nível de dificuldade para a manutenibilidade. 

Para resolver isso utilizamos abstrações. 

Primeiro a gente cria uma interface, para que todos os nossos métodos de pagamento possam implementar ela com seus devidos processos individuais

```
public interface IOrderPaymentMethod 
{
	void Process();
}
```

Agora vamos criar uma classe para cada método de pagamento:

```
public class CredtCardMethod : IOrderPaymentMethod 
{
	public void Process()
	{
		//Processamento B
	}
}
```

```
public class DebitCardMethod : IOrderPaymentMethod 
{
	public void Process()
	{
		//Processamento B
	}
}
```

Desse jeito, a classe que antes tinha aquela estrutura toda de if e if else passa a simplesmente esperar receber um IOrderPaymentMethod e retornar o processamento, desse jeito: 

```
public class PaymentService 
{
	public void Process(IOrderPaymentMethod paymentMethod)
	{
		paymentMethod.Process()
	}
}
```

Como todos as classes implementam essa interface, então qualquer uma delas pode ser passado como parâmetro. 

Surgindo um método novo de pagamento é só adicionar mais uma classe que implementa a interface e a estrutura que já funcionava anteriormente não precisa receber nenhum tipo de modificação

```
public class PixMethod : IOrderPaymentMethod 
{
	public void Process()
	{
		//Processamento C
	}
}
```

Como podemos observar, ela se tornou extensível mas que, em caso de novas funcionalidades relacionadas, o código existente não está sendo alterado.

#### Liskov's Substitution Principle - LSP

Frase que representa o principio: “Subclasses should be substitutable for their base classes”

Esse principio é infringido quando a herança é mal utilizada. 

Geralmente utilizamos herança quando duas classes tem campos e métodos em comum e não queremos reescrever tudo novamente, então utilizamos da herança para reaproveitamento de código. 

Porém, esse reaproveitamento pode gerar uma brecha no sistema. 

##### Exemplo:
Contexto: 
Imagine que você está desenvolvendo um sistema bancário que possui uma classe base `ContaBancaria`, da qual herdam `ContaCorrente` e `ContaPoupanca`.

A classe `ContaBancaria` possui métodos para `Depositar()` e `Sacar()`, que permitem adicionar e remover valores do saldo da conta:
```
public class ContaBancaria
{
    public decimal Saldo { get; protected set; }

    public virtual void Depositar(decimal valor) => Saldo += valor;

    public virtual void Sacar(decimal valor)
    {
        if (Saldo >= valor)
            Saldo -= valor;
        else
            throw new InvalidOperationException("Saldo insuficiente");
    }
}
```

Agora, suponha que você cria uma classe `ContaPoupanca` que herda de `ContaBancaria`. No entanto, contas poupança geralmente têm restrições de saque, como permitir saques apenas uma vez por mês ou com uma taxa adicional. Para implementar isso, você substitui o método `Sacar()`:

```
public class ContaPoupanca : ContaBancaria
{
    public int NumeroDeSaques { get; private set; }
    private const int LimiteDeSaques = 1;

    public override void Sacar(decimal valor)
    {
        if (NumeroDeSaques >= LimiteDeSaques)
            throw new InvalidOperationException("Limite de saques atingido para este mês");

        base.Sacar(valor);
        NumeroDeSaques++;
    }
}
```

O LSP afirma que, se `ContaPoupanca` é uma subtipo de `ContaBancaria`, deveríamos poder substituir qualquer instância de `ContaBancaria` por uma `ContaPoupanca` **sem alterar o comportamento esperado.** No entanto, se um cliente do sistema tenta realizar saques repetidos em uma `ContaPoupanca`, a implementação personalizada para `Sacar()` irá lançar uma exceção (quando o limite de saques for excedido), violando o comportamento original de `ContaBancaria`, que não possui esse tipo de restrição.

Se temos uma função que opera sobre uma `ContaBancaria` e espera poder sacar múltiplas vezes, o comportamento será diferente para `ContaPoupanca`. Essa violação do LSP pode introduzir erros em partes do sistema que esperavam que todas as `ContaBancaria` pudessem realizar saques ilimitados (desde que haja saldo), mas descobrem que `ContaPoupanca` adiciona restrições não previstas.

Para realmente evitar essa violação do LSP, precisamos separar os comportamentos específicos de cada tipo de conta. Em vez de usar herança para `ContaPoupanca`, podemos:

1. **Remover a herança direta entre `ContaPoupanca` e `ContaBancaria`**.
2. **Criar uma estrutura de composição**, onde `ContaPoupanca` e `ContaCorrente` são classes que têm uma `ContaBancaria` embutida (como um campo interno), permitindo que os métodos sejam adaptados sem alterar o comportamento original da classe base.

```
public class ContaBancaria
{
    public decimal Saldo { get; protected set; }

    public void Depositar(decimal valor) => Saldo += valor;
    
    public void Sacar(decimal valor)
    {
        if (Saldo >= valor)
            Saldo -= valor;
        else
            throw new InvalidOperationException("Saldo insuficiente");
    }
}
```

```
public interface IContaSaque
{
    void Sacar(decimal valor);
}
```

```
public class ContaCorrente : IContaSaque
{
    private readonly ContaBancaria contaBancaria;

    public ContaCorrente()
    {
        contaBancaria = new ContaBancaria();
    }

    public void Depositar(decimal valor) => contaBancaria.Depositar(valor);

    public void Sacar(decimal valor) => contaBancaria.Sacar(valor);
}
```

```
public class ContaPoupanca : IContaSaque
{
    private readonly ContaBancaria contaBancaria;
    public int NumeroDeSaques { get; private set; }
    private const int LimiteDeSaques = 1;

    public ContaPoupanca()
    {
        contaBancaria = new ContaBancaria();
    }

    public void Depositar(decimal valor) => contaBancaria.Depositar(valor);

    public void Sacar(decimal valor)
    {
        if (NumeroDeSaques >= LimiteDeSaques)
            throw new InvalidOperationException("Limite de saques atingido para este mês");

        contaBancaria.Sacar(valor);
        NumeroDeSaques++;
    }
}
```

Essa abordagem elimina a violação do LSP, pois `ContaPoupanca` e `ContaCorrente` agora não substituem diretamente `ContaBancaria`, mas sim compartilham comportamento via composição. Isso permite que `ContaCorrente` e `ContaPoupanca` possam ser usadas de acordo com suas restrições específicas sem quebrar o comportamento esperado de uma `ContaBancaria` comum.

A composição permite que a `ContaBancaria` continue com o gerenciamento do saldo, permitindo que as classes apliquem as restrições de saque de forma independente.

Em relações de "é-um" no uso de herança, além de garantir que a classe derivada representa genuinamente um tipo específico da classe base, também é fundamental que o **comportamento da classe derivada não quebre o comportamento esperado da classe base**.
#### Interface Segregation Principle - ISP

Frase que representa o principio: “Clients should not be forced to depend upon interfaces that they do not use”

É basicamente que a classe não deve ser forçada a depender de interfaces que ela não vai utilizar, ou seja, ela precisa herdar de uma interface que tem vários métodos para serem implementados porém ela só precisa de parte desses métodos. Logo, alguns teriam o NotImplementedException. 

Uma situação bem comum que fere esse principio é em casos de repositórios genéricos que geralmente tem os métodos do crud completo neles, exemplo: 

```
public interface IRepository<T>
{
	void Add(T entitiy);
	void Update(T entity);
	void Delete(Guid id);
	IEnumerable<T> GetAll();
	T GetById(Guid int);
}
```

Em algumas situações nós temos casos em que é necessário somente o retorno dos dados, uma classe A não necessariamente tem o crud inteiro, o insert dos dados pode ter sido feito via carga direta ao banco de dados. 

Ao implementar a busca dos dados utilizando essa interface genérica, os métodos de alteração de dados iriam ficar "sobrando" e não teria implementação deles 

```
public class EntidadeARepository : IRepository<EntidadeA>
{
	public void Add(EntidadeA entitiy) 
	{
		throw new NotImplementedException();
	}
	
	public void Update(EntidadeA entitiy) 
	{
		throw new NotImplementedException();
	}
	
	public void Delete(EntidadeA entitiy) 
	{
		throw new NotImplementedException();
	}

	public IEnumerable<EntidadeA> GetAll()
	{
		return dbContext.EntidadeA.GetAll();
	}
	
	public EntidadeA GetById(Guid id)
	{
		return dbContext.EntidadeA.GetAll().FirstOrDefault(a => a.Id == id);
	}
}
```

Isso fere completamente o principio. 

Para resolver essa situação, podemos por exemplo ter classes genéricas diferentes para consulta e para alteração de dados. 

```
public interface IWriteRepository<T>
{
	void Add(T entitiy);
	void Update(T entity);
	void Delete(Guid id);
}
```

```
public interface IReadRepository<T>
{
	IEnumerable<T> GetAll();
	T GetById(Guid int);
}
```

Dessa forma, a nossa classe EntidadeA pode implementar só a IReadRepository reduzindo consideravelmente o código

```
public class EntidadeARepository : IReadRepository<EntidadeA>
{
	public IEnumerable<EntidadeA> GetAll()
	{
		return dbContext.EntidadeA.GetAll();
	}
	
	public EntidadeA GetById(Guid id)
	{
		return dbContext.EntidadeA.GetAll().FirstOrDefault(a => a.Id == id);
	}
}
```

#### Dependency Inversion Principle - DIP

Frase que representa o principio: “High-level modules should not depend upon low-leve modules. Both should depend upon abstractions.”

Programe para interfaces e não para implementações 

Isso irá ajudar a reduzir o acoplamento entre as partes do sistema, facilitando a extensão, modificação e teste do código

Quando vamos criar uma classe de serviço, geralmente precisamos ter acesso a varias outras coisas como por exemplo ao banco de dados 

Algo assim: 
```
public class EntidadeService
{
	private EntidadeRepository repository; 
	
	EntidadeService(EntidadeRepository repository){
		this.repository = repository; 
	}

	public void Add(Entidade entidade)
	{
		this.repository.add(entidade)
	}
}
```

Desse jeito estamos ferindo o principio porque estamos injetando a implementação direta do `EntidadeRepository`

Isso é um problema porque ao invés de depender diretamente de uma implementação concreta como `EntidadeRepository` ela pode depender de uma interface `IEntidadeRepository`, permitindo que o serviço funcione com qualquer classe que implemente essa interface, seja para salvar em um banco de dados, em um arquivo ou até mesmo na nuvem.

Quando as classes dependem de abstrações, você pode alterar as implementações concretas sem afetar o código que as utiliza, desde que o contrato da interface permaneça o mesmo. Esse isolamento de mudanças facilita a evolução do sistema ao longo do tempo.

Além disso, programar para interfaces favorece o **Open/Closed Principle**. Com interfaces, novas implementações podem ser adicionadas sem a necessidade de modificar o código existente, o que promove extensibilidade sem interferir no que já está funcionando.

Ficando desse jeito 
```
public interface IEntidadeRepository
{
	void Add(Entidade entitidade);
}
```

```
public class EntidadeService
{
	private IEntidadeRepository repository; 
	
	EntidadeService(IEntidadeRepository repository){
		this.repository = repository; 
	}

	public void Add(Entidade entidade)
	{
		this.repository.add(entidade)
	}
}
```

E a classe `EntidadeRepository` implementa a `IEntidadeRepository`.

```
public class EntidadeRepository : IEntidadeRepository 
{
	public void Add(Entidade entidade)
	{
		//Implementação
	}
}
```
