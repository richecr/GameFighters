using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    private float velocidade;
    private float forcaPulo;
    private bool podePular;
    private Transform transformHero;
    private Rigidbody2D rigidbody;
    private Animator animator;
    public CapsuleCollider2D arma;
    public int vida;

	// Use this for initialization
	void Start () {
        this.velocidade = 5;
        this.forcaPulo = 9;
        this.vida = 100;
        this.podePular = false;
        this.transformHero = GetComponent<Transform>();
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.arma = GetComponent<CapsuleCollider2D>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D)) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(KeyCode.A)) {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        // Correr.
        if (Input.GetKey(KeyCode.D)) {
            this.transform.Translate(new Vector2(this.velocidade * Time.deltaTime, 0));
            this.animacaoRun();
        } else if (Input.GetKey(KeyCode.A)) {
            this.transform.Translate(new Vector2(-this.velocidade * Time.deltaTime, 0));
            this.animacaoRun();
        } else if ((!Input.GetKey(KeyCode.A)) && (!Input.GetKey(KeyCode.D))) {
            this.animacaoParar();
        }

        // Pular.
        if (Input.GetKeyDown(KeyCode.Space) && (this.podePular) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
            this.rigidbody.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
            this.animacaoPular();
        } else if (Input.GetKeyDown(KeyCode.Space) && (this.podePular) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
            this.rigidbody.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
            this.animacaoPular();
        }

        // Ataca.
        if (Input.GetKey(KeyCode.K) && podePular) {
            this.arma.isTrigger = false;
            this.animacaoAtaca();
        } else if (Input.GetKeyDown(KeyCode.K)) {
            this.arma.isTrigger = true;
            this.animacaoParar();
        }
    }

    private void animacaoRun() {
        this.animator.SetBool("idle", false);
        this.animator.SetBool("run", true);
    }

    private void animacaoParar() {
        this.animator.SetBool("attack", false);
        this.animator.SetBool("run", false);
        this.animator.SetBool("idle", true);
    }

    private void animacaoAtaca() {
        this.animator.SetBool("run", false);
        this.animator.SetBool("jump", false);
        this.animator.SetBool("idle", false);
        this.animator.SetBool("attack", true);
    }

    private void animacaoPular() {
        this.animator.SetBool("idle", false);
        this.animator.SetBool("run", false);
        this.animator.SetBool("jump", true);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag("Chao")) {
            this.podePular = true;
            this.animator.SetBool("jump", false);
            this.animator.SetBool("idle", true);
        }

        if (collision.gameObject.CompareTag("Arvore")) {
            this.vida -= 10;
        }

    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Chao")) {
            this.podePular = false;
        }
    }
}
