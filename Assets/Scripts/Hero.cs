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
        this.velocidade = 0;
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

        // Ataca.
        if (Input.GetKey(KeyCode.K) && podePular) {
            this.arma.isTrigger = false;
            this.animacaoAtaca();
        } else if (Input.GetKeyDown(KeyCode.K)) {
            this.arma.isTrigger = true;
            this.animacaoParar();
        }

        this.mover();
    }

    public void pular(){
        if (podePular){
            this.animacaoPular();
            this.rigidbody.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
        } else {
            this.animacaoParar();
        }
    }

    public void direita(){
        GetComponent<SpriteRenderer>().flipX = false;
        this.velocidade = 5;
    }

    public void esquerda(){
        GetComponent<SpriteRenderer>().flipX = true;
        this.velocidade = -5;

    }

    public void parado(){
        this.velocidade = 0;
        this.animacaoParar();
    }

    private void mover(){
        if (this.velocidade != 0){
            this.animacaoRun();
            this.transform.Translate(new Vector2(this.velocidade * Time.deltaTime, 0));
        }
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
}