﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strategy_Pagamentos.Model.FormasDePagameto;
using Strategy_Pagamentos.Model.Preco;

namespace Strategy_Pagamentos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrecoController : ControllerBase
    {
        private readonly IPreco _preco;

        public PrecoController(IPreco preco)
        {
            _preco = preco;
        }

        [HttpGet("{id}")]
        public JsonResult PrecoPorFormaDePagamento(int id)
        {
            List<PrecoDTO> precos = new List<PrecoDTO>();

            PrecoDTO precoBoletoDTO = new PrecoDTO();
            precoBoletoDTO.ProdutoID = id;
            precoBoletoDTO.FormaDePagamento = FormasDePagamentoENUM.PagamentoBoleto;
            precoBoletoDTO.DescricaoDaFormaDePagamento = "Boleto";
            precoBoletoDTO.PrecoBase = _preco.PrecoBasePorID(precoBoletoDTO);
            precoBoletoDTO.PrecoFinal = _preco.PrecoPorFormaDePagamento(precoBoletoDTO); // Strategy aqui
            precos.Add(precoBoletoDTO);

            PrecoDTO precoCartaoCreditoDTO = new PrecoDTO();
            precoCartaoCreditoDTO.ProdutoID = id;
            precoCartaoCreditoDTO.FormaDePagamento = FormasDePagamentoENUM.PagamentoCartaoCredito;
            precoCartaoCreditoDTO.DescricaoDaFormaDePagamento = "Cartao de Credito";
            precoCartaoCreditoDTO.PrecoBase = _preco.PrecoBasePorID(precoCartaoCreditoDTO);
            precoCartaoCreditoDTO.PrecoFinal = _preco.PrecoPorFormaDePagamento(precoCartaoCreditoDTO); // Strategy aqui
            precos.Add(precoCartaoCreditoDTO);

            PrecoDTO precoCartaoDebitoDTO = new PrecoDTO();
            precoCartaoDebitoDTO.ProdutoID = id;
            precoCartaoDebitoDTO.FormaDePagamento = FormasDePagamentoENUM.PagamentoCartaoDebito;
            precoCartaoDebitoDTO.DescricaoDaFormaDePagamento = "Cartao de Debito";
            precoCartaoDebitoDTO.PrecoBase = _preco.PrecoBasePorID(precoCartaoDebitoDTO);
            precoCartaoDebitoDTO.PrecoFinal = _preco.PrecoPorFormaDePagamento(precoCartaoDebitoDTO); // Strategy aqui
            precos.Add(precoCartaoDebitoDTO);

            return new JsonResult(precos);
        }
    }
}