


<!DOCTYPE html>
<html>
  <head prefix="og: http://ogp.me/ns# fb: http://ogp.me/ns/fb# githubog: http://ogp.me/ns/fb/githubog#">
    <meta charset='utf-8'>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>bootstrap/docs/assets/js/html5shiv.js at master · twbs/bootstrap · GitHub</title>
    <link rel="search" type="application/opensearchdescription+xml" href="/opensearch.xml" title="GitHub" />
    <link rel="fluid-icon" href="https://github.com/fluidicon.png" title="GitHub" />
    <link rel="apple-touch-icon" sizes="57x57" href="/apple-touch-icon-114.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="/apple-touch-icon-114.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="/apple-touch-icon-144.png" />
    <link rel="apple-touch-icon" sizes="144x144" href="/apple-touch-icon-144.png" />
    <link rel="logo" type="image/svg" href="https://github-media-downloads.s3.amazonaws.com/github-logo.svg" />
    <meta property="og:image" content="https://github.global.ssl.fastly.net/images/modules/logos_page/Octocat.png">
    <meta name="hostname" content="fe2.rs.github.com">
    <meta name="ruby" content="ruby 1.9.3p194-tcs-github-tcmalloc (2012-05-25, TCS patched 2012-05-27, GitHub v1.0.32) [x86_64-linux]">
    <link rel="assets" href="https://github.global.ssl.fastly.net/">
    <link rel="xhr-socket" href="/_sockets" />
    
    


    <meta name="msapplication-TileImage" content="/windows-tile.png" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="selected-link" value="repo_source" data-pjax-transient />
    <meta content="collector.githubapp.com" name="octolytics-host" /><meta content="github" name="octolytics-app-id" />

    
    
    <link rel="icon" type="image/x-icon" href="/favicon.ico" />

    <meta content="authenticity_token" name="csrf-param" />
<meta content="Hj508eqm/Cky0dgJmB3S+mHQi1a3Rsz3l0i/41g6FEo=" name="csrf-token" />

    <link href="https://github.global.ssl.fastly.net/assets/github-4eb23d23b58990e75746e6796c0952b24a9c8b21.css" media="all" rel="stylesheet" type="text/css" />
    <link href="https://github.global.ssl.fastly.net/assets/github2-d3cdff01428383b2670dce9b781434be7c9d37e8.css" media="all" rel="stylesheet" type="text/css" />
    


      <script src="https://github.global.ssl.fastly.net/assets/frameworks-eae23340ab2a6ba722166712e699c87aaf60ad8f.js" type="text/javascript"></script>
      <script src="https://github.global.ssl.fastly.net/assets/github-a4de942c73c48e278373120f94de228c91dde62a.js" type="text/javascript"></script>
      
      <meta http-equiv="x-pjax-version" content="bfbc4ab7075897d2673267b50758e6b1">

        <link data-pjax-transient rel='permalink' href='/twbs/bootstrap/blob/7f0b487477bc63bef488dff14a0c28ea2106286b/docs/assets/js/html5shiv.js'>
  <meta property="og:title" content="bootstrap"/>
  <meta property="og:type" content="githubog:gitrepository"/>
  <meta property="og:url" content="https://github.com/twbs/bootstrap"/>
  <meta property="og:image" content="https://github.global.ssl.fastly.net/images/gravatars/gravatar-user-420.png"/>
  <meta property="og:site_name" content="GitHub"/>
  <meta property="og:description" content="bootstrap - Sleek, intuitive, and powerful front-end framework for faster and easier web development."/>

  <meta name="description" content="bootstrap - Sleek, intuitive, and powerful front-end framework for faster and easier web development." />

  <meta content="2918581" name="octolytics-dimension-user_id" /><meta content="twbs" name="octolytics-dimension-user_login" /><meta content="2126244" name="octolytics-dimension-repository_id" /><meta content="twbs/bootstrap" name="octolytics-dimension-repository_nwo" /><meta content="true" name="octolytics-dimension-repository_public" /><meta content="false" name="octolytics-dimension-repository_is_fork" /><meta content="2126244" name="octolytics-dimension-repository_network_root_id" /><meta content="twbs/bootstrap" name="octolytics-dimension-repository_network_root_nwo" />
  <link href="https://github.com/twbs/bootstrap/commits/master.atom" rel="alternate" title="Recent Commits to bootstrap:master" type="application/atom+xml" />

  </head>


  <body class="logged_out page-blob windows vis-public env-production ">

    <div class="wrapper">
      
      
      


      
      <div class="header header-logged-out">
  <div class="container clearfix">

    <a class="header-logo-wordmark" href="https://github.com/">
      <span class="mega-octicon octicon-logo-github"></span>
    </a>

    <div class="header-actions">
        <a class="button primary" href="/signup">Sign up</a>
      <a class="button" href="/login?return_to=%2Ftwbs%2Fbootstrap%2Fblob%2Fmaster%2Fdocs%2Fassets%2Fjs%2Fhtml5shiv.js">Sign in</a>
    </div>

    <div class="command-bar js-command-bar  in-repository">


      <ul class="top-nav">
          <li class="explore"><a href="/explore">Explore</a></li>
        <li class="features"><a href="/features">Features</a></li>
          <li class="enterprise"><a href="https://enterprise.github.com/">Enterprise</a></li>
          <li class="blog"><a href="/blog">Blog</a></li>
      </ul>
        <form accept-charset="UTF-8" action="/search" class="command-bar-form" id="top_search_form" method="get">

<input type="text" data-hotkey="/ s" name="q" id="js-command-bar-field" placeholder="Search or type a command" tabindex="1" autocapitalize="off"
    
    
      data-repo="twbs/bootstrap"
      data-branch="master"
      data-sha="ba547d4cce7f898428a974f18b8c3bb66777fc9a"
  >

    <input type="hidden" name="nwo" value="twbs/bootstrap" />

    <div class="select-menu js-menu-container js-select-menu search-context-select-menu">
      <span class="minibutton select-menu-button js-menu-target">
        <span class="js-select-button">This repository</span>
      </span>

      <div class="select-menu-modal-holder js-menu-content js-navigation-container">
        <div class="select-menu-modal">

          <div class="select-menu-item js-navigation-item js-this-repository-navigation-item selected">
            <span class="select-menu-item-icon octicon octicon-check"></span>
            <input type="radio" class="js-search-this-repository" name="search_target" value="repository" checked="checked" />
            <div class="select-menu-item-text js-select-button-text">This repository</div>
          </div> <!-- /.select-menu-item -->

          <div class="select-menu-item js-navigation-item js-all-repositories-navigation-item">
            <span class="select-menu-item-icon octicon octicon-check"></span>
            <input type="radio" name="search_target" value="global" />
            <div class="select-menu-item-text js-select-button-text">All repositories</div>
          </div> <!-- /.select-menu-item -->

        </div>
      </div>
    </div>

  <span class="octicon help tooltipped downwards" title="Show command bar help">
    <span class="octicon octicon-question"></span>
  </span>


  <input type="hidden" name="ref" value="cmdform">

</form>
    </div>

  </div>
</div>


      


          <div class="site" itemscope itemtype="http://schema.org/WebPage">
    
    <div class="pagehead repohead instapaper_ignore readability-menu">
      <div class="container">
        

<ul class="pagehead-actions">


  <li>
  <a href="/login?return_to=%2Ftwbs%2Fbootstrap"
  class="minibutton with-count js-toggler-target star-button entice tooltipped upwards "
  title="You must be signed in to use this feature" rel="nofollow">
  <span class="octicon octicon-star"></span>Star
</a>
<a class="social-count js-social-count" href="/twbs/bootstrap/stargazers">
  55,675
</a>

  </li>

    <li>
      <a href="/login?return_to=%2Ftwbs%2Fbootstrap"
        class="minibutton with-count js-toggler-target fork-button entice tooltipped upwards"
        title="You must be signed in to fork a repository" rel="nofollow">
        <span class="octicon octicon-git-branch"></span>Fork
      </a>
      <a href="/twbs/bootstrap/network" class="social-count">
        18,972
      </a>
    </li>
</ul>

        <h1 itemscope itemtype="http://data-vocabulary.org/Breadcrumb" class="entry-title public">
          <span class="repo-label"><span>public</span></span>
          <span class="mega-octicon octicon-repo"></span>
          <span class="author">
            <a href="/twbs" class="url fn" itemprop="url" rel="author"><span itemprop="title">twbs</span></a></span
          ><span class="repohead-name-divider">/</span><strong
          ><a href="/twbs/bootstrap" class="js-current-repository js-repo-home-link">bootstrap</a></strong>

          <span class="page-context-loader">
            <img alt="Octocat-spinner-32" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
          </span>

        </h1>
      </div><!-- /.container -->
    </div><!-- /.repohead -->

    <div class="container">

      <div class="repository-with-sidebar repo-container ">

        <div class="repository-sidebar">
            

<div class="repo-nav repo-nav-full js-repository-container-pjax js-octicon-loaders">
  <div class="repo-nav-contents">
    <ul class="repo-menu">
      <li class="tooltipped leftwards" title="Code">
        <a href="/twbs/bootstrap/tree/master" aria-label="Code" class="js-selected-navigation-item selected" data-gotokey="c" data-pjax="true" data-selected-links="repo_source repo_downloads repo_commits repo_tags repo_branches /twbs/bootstrap/tree/master">
          <span class="octicon octicon-code"></span> <span class="full-word">Code</span>
          <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>

        <li class="tooltipped leftwards" title="Issues">
          <a href="/twbs/bootstrap/issues" aria-label="Issues" class="js-selected-navigation-item js-disable-pjax" data-gotokey="i" data-selected-links="repo_issues /twbs/bootstrap/issues">
            <span class="octicon octicon-issue-opened"></span> <span class="full-word">Issues</span>
            <span class='counter'>30</span>
            <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>        </li>

      <li class="tooltipped leftwards" title="Pull Requests"><a href="/twbs/bootstrap/pulls" aria-label="Pull Requests" class="js-selected-navigation-item js-disable-pjax" data-gotokey="p" data-selected-links="repo_pulls /twbs/bootstrap/pulls">
            <span class="octicon octicon-git-pull-request"></span> <span class="full-word">Pull Requests</span>
            <span class='counter'>8</span>
            <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>


    </ul>
    <div class="repo-menu-separator"></div>
    <ul class="repo-menu">

      <li class="tooltipped leftwards" title="Pulse">
        <a href="/twbs/bootstrap/pulse" aria-label="Pulse" class="js-selected-navigation-item " data-pjax="true" data-selected-links="pulse /twbs/bootstrap/pulse">
          <span class="octicon octicon-pulse"></span> <span class="full-word">Pulse</span>
          <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>

      <li class="tooltipped leftwards" title="Graphs">
        <a href="/twbs/bootstrap/graphs" aria-label="Graphs" class="js-selected-navigation-item " data-pjax="true" data-selected-links="repo_graphs repo_contributors /twbs/bootstrap/graphs">
          <span class="octicon octicon-graph"></span> <span class="full-word">Graphs</span>
          <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>

      <li class="tooltipped leftwards" title="Network">
        <a href="/twbs/bootstrap/network" aria-label="Network" class="js-selected-navigation-item js-disable-pjax" data-selected-links="repo_network /twbs/bootstrap/network">
          <span class="octicon octicon-git-branch"></span> <span class="full-word">Network</span>
          <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>

    </ul>

  </div>
</div>

            <div class="only-with-full-nav">
              

  

<div class="clone-url open"
  data-protocol-type="http"
  data-url="/users/set_protocol?protocol_selector=http&amp;protocol_type=clone">
  <h3><strong>HTTPS</strong> clone URL</h3>

  <input type="text" class="clone js-url-field"
         value="https://github.com/twbs/bootstrap.git" readonly="readonly">

  <span class="js-zeroclipboard url-box-clippy minibutton zeroclipboard-button" data-clipboard-text="https://github.com/twbs/bootstrap.git" data-copied-hint="copied!" title="copy to clipboard"><span class="octicon octicon-clippy"></span></span>
</div>

  

<div class="clone-url "
  data-protocol-type="subversion"
  data-url="/users/set_protocol?protocol_selector=subversion&amp;protocol_type=clone">
  <h3><strong>Subversion</strong> checkout URL</h3>

  <input type="text" class="clone js-url-field"
         value="https://github.com/twbs/bootstrap" readonly="readonly">

  <span class="js-zeroclipboard url-box-clippy minibutton zeroclipboard-button" data-clipboard-text="https://github.com/twbs/bootstrap" data-copied-hint="copied!" title="copy to clipboard"><span class="octicon octicon-clippy"></span></span>
</div>



<p class="clone-options">You can clone with
    <a href="#" class="js-clone-selector" data-protocol="http">HTTPS</a>,
    <a href="#" class="js-clone-selector" data-protocol="subversion">Subversion</a>,
  and <a href="https://help.github.com/articles/which-remote-url-should-i-use">other methods.</a>
</p>


  <a href="http://windows.github.com" class="minibutton sidebar-button">
    <span class="octicon octicon-device-desktop"></span>
    Clone in Desktop
  </a>

                <a href="/twbs/bootstrap/archive/master.zip"
                   class="minibutton sidebar-button"
                   title="Download this repository as a zip file"
                   rel="nofollow">
                  <span class="octicon octicon-cloud-download"></span>
                  Download ZIP
                </a>
            </div>
        </div><!-- /.repository-sidebar -->

        <div id="js-repo-pjax-container" class="repository-content context-loader-container" data-pjax-container>
          


<!-- blob contrib key: blob_contributors:v21:1d355ee8cbbb74163bceed6506ac9cbe -->
<!-- blob contrib frag key: views10/v8/blob_contributors:v21:1d355ee8cbbb74163bceed6506ac9cbe -->

<p title="This is a placeholder element" class="js-history-link-replace hidden"></p>

<a href="/twbs/bootstrap/find/master" data-pjax data-hotkey="t" style="display:none">Show File Finder</a>

<div class="file-navigation">
  


<div class="select-menu js-menu-container js-select-menu" >
  <span class="minibutton select-menu-button js-menu-target" data-hotkey="w"
    data-master-branch="3.0.0-wip"
    data-ref="master">
    <span class="octicon octicon-git-branch"></span>
    <i>branch:</i>
    <span class="js-select-button">master</span>
  </span>

  <div class="select-menu-modal-holder js-menu-content js-navigation-container" data-pjax>

    <div class="select-menu-modal">
      <div class="select-menu-header">
        <span class="select-menu-title">Switch branches/tags</span>
        <span class="octicon octicon-remove-close js-menu-close"></span>
      </div> <!-- /.select-menu-header -->

      <div class="select-menu-filters">
        <div class="select-menu-text-filter">
          <input type="text" id="context-commitish-filter-field" class="js-filterable-field js-navigation-enable" placeholder="Filter branches/tags">
        </div>
        <div class="select-menu-tabs">
          <ul>
            <li class="select-menu-tab">
              <a href="#" data-tab-filter="branches" class="js-select-menu-tab">Branches</a>
            </li>
            <li class="select-menu-tab">
              <a href="#" data-tab-filter="tags" class="js-select-menu-tab">Tags</a>
            </li>
          </ul>
        </div><!-- /.select-menu-tabs -->
      </div><!-- /.select-menu-filters -->

      <div class="select-menu-list select-menu-tab-bucket js-select-menu-tab-bucket" data-tab-filter="branches">

        <div data-filterable-for="context-commitish-filter-field" data-filterable-type="substring">


            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/3.0.0-wip/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="3.0.0-wip" data-skip-pjax="true" rel="nofollow" title="3.0.0-wip">3.0.0-wip</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/bs3_navbar_overhaul/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="bs3_navbar_overhaul" data-skip-pjax="true" rel="nofollow" title="bs3_navbar_overhaul">bs3_navbar_overhaul</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/gh-pages/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="gh-pages" data-skip-pjax="true" rel="nofollow" title="gh-pages">gh-pages</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item selected">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/master/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="master" data-skip-pjax="true" rel="nofollow" title="master">master</a>
            </div> <!-- /.select-menu-item -->
        </div>

          <div class="select-menu-no-results">Nothing to show</div>
      </div> <!-- /.select-menu-list -->

      <div class="select-menu-list select-menu-tab-bucket js-select-menu-tab-bucket" data-tab-filter="tags">
        <div data-filterable-for="context-commitish-filter-field" data-filterable-type="substring">


            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v3.0.0-rc1/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v3.0.0-rc1" data-skip-pjax="true" rel="nofollow" title="v3.0.0-rc1">v3.0.0-rc1</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.3.2/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.3.2" data-skip-pjax="true" rel="nofollow" title="v2.3.2">v2.3.2</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.3.1/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.3.1" data-skip-pjax="true" rel="nofollow" title="v2.3.1">v2.3.1</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.3.0/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.3.0" data-skip-pjax="true" rel="nofollow" title="v2.3.0">v2.3.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.2.2/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.2.2" data-skip-pjax="true" rel="nofollow" title="v2.2.2">v2.2.2</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.2.1/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.2.1" data-skip-pjax="true" rel="nofollow" title="v2.2.1">v2.2.1</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.2.0/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.2.0" data-skip-pjax="true" rel="nofollow" title="v2.2.0">v2.2.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.1.1/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.1.1" data-skip-pjax="true" rel="nofollow" title="v2.1.1">v2.1.1</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.1.0/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.1.0" data-skip-pjax="true" rel="nofollow" title="v2.1.0">v2.1.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.0.4/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.0.4" data-skip-pjax="true" rel="nofollow" title="v2.0.4">v2.0.4</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.0.3/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.0.3" data-skip-pjax="true" rel="nofollow" title="v2.0.3">v2.0.3</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.0.2/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.0.2" data-skip-pjax="true" rel="nofollow" title="v2.0.2">v2.0.2</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.0.1/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.0.1" data-skip-pjax="true" rel="nofollow" title="v2.0.1">v2.0.1</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v2.0.0/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v2.0.0" data-skip-pjax="true" rel="nofollow" title="v2.0.0">v2.0.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v1.4.0/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v1.4.0" data-skip-pjax="true" rel="nofollow" title="v1.4.0">v1.4.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v1.3.0/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v1.3.0" data-skip-pjax="true" rel="nofollow" title="v1.3.0">v1.3.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v1.2.0/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v1.2.0" data-skip-pjax="true" rel="nofollow" title="v1.2.0">v1.2.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v1.1.1/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v1.1.1" data-skip-pjax="true" rel="nofollow" title="v1.1.1">v1.1.1</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v1.1.0/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v1.1.0" data-skip-pjax="true" rel="nofollow" title="v1.1.0">v1.1.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/twbs/bootstrap/blob/v1.0.0/docs/assets/js/html5shiv.js" class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target" data-name="v1.0.0" data-skip-pjax="true" rel="nofollow" title="v1.0.0">v1.0.0</a>
            </div> <!-- /.select-menu-item -->
        </div>

        <div class="select-menu-no-results">Nothing to show</div>
      </div> <!-- /.select-menu-list -->

    </div> <!-- /.select-menu-modal -->
  </div> <!-- /.select-menu-modal-holder -->
</div> <!-- /.select-menu -->

  <div class="breadcrumb">
    <span class='repo-root js-repo-root'><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/twbs/bootstrap/tree/master" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">bootstrap</span></a></span></span><span class="separator"> / </span><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/twbs/bootstrap/tree/master/docs" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">docs</span></a></span><span class="separator"> / </span><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/twbs/bootstrap/tree/master/docs/assets" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">assets</span></a></span><span class="separator"> / </span><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/twbs/bootstrap/tree/master/docs/assets/js" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">js</span></a></span><span class="separator"> / </span><strong class="final-path">html5shiv.js</strong> <span class="js-zeroclipboard minibutton zeroclipboard-button" data-clipboard-text="docs/assets/js/html5shiv.js" data-copied-hint="copied!" title="copy to clipboard"><span class="octicon octicon-clippy"></span></span>
  </div>
</div>


  <div class="commit commit-loader file-history-tease js-deferred-content" data-url="/twbs/bootstrap/contributors/master/docs/assets/js/html5shiv.js">
    Fetching contributors…

    <div class="participation">
      <p class="loader-loading"><img alt="Octocat-spinner-32-eaf2f5" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32-EAF2F5.gif" width="16" /></p>
      <p class="loader-error">Cannot retrieve contributors at this time</p>
    </div>
  </div>

<div id="files" class="bubble">
  <div class="file">
    <div class="meta">
      <div class="info">
        <span class="icon"><b class="octicon octicon-file-text"></b></span>
        <span class="mode" title="File Mode">file</span>
          <span>9 lines (8 sloc)</span>
        <span>2.376 kb</span>
      </div>
      <div class="actions">
        <div class="button-group">
              <a class="minibutton js-entice" href=""
                 data-entice="You must be signed in to make or propose changes">Edit</a>
          <a href="/twbs/bootstrap/raw/master/docs/assets/js/html5shiv.js" class="button minibutton " id="raw-url">Raw</a>
            <a href="/twbs/bootstrap/blame/master/docs/assets/js/html5shiv.js" class="button minibutton ">Blame</a>
          <a href="/twbs/bootstrap/commits/master/docs/assets/js/html5shiv.js" class="button minibutton " rel="nofollow">History</a>
        </div><!-- /.button-group -->
            <a class="minibutton danger empty-icon js-entice" href=""
               data-entice="You must be signed in and on a branch to make or propose changes">
            Delete
          </a>
      </div><!-- /.actions -->

    </div>
        <div class="blob-wrapper data type-javascript js-blob-data">
      <table class="file-code file-diff">
        <tr class="file-code-line">
          <td class="blob-line-nums">
            <span id="L1" rel="#L1">1</span>
<span id="L2" rel="#L2">2</span>
<span id="L3" rel="#L3">3</span>
<span id="L4" rel="#L4">4</span>
<span id="L5" rel="#L5">5</span>
<span id="L6" rel="#L6">6</span>
<span id="L7" rel="#L7">7</span>
<span id="L8" rel="#L8">8</span>

          </td>
          <td class="blob-line-code">
                  <div class="highlight"><pre><div class='line' id='LC1'><span class="cm">/*</span></div><div class='line' id='LC2'><span class="cm"> HTML5 Shiv v3.6.2pre | @afarkas @jdalton @jon_neal @rem | MIT/GPL2 Licensed</span></div><div class='line' id='LC3'><span class="cm">*/</span></div><div class='line' id='LC4'><span class="p">(</span><span class="kd">function</span><span class="p">(</span><span class="nx">l</span><span class="p">,</span><span class="nx">f</span><span class="p">){</span><span class="kd">function</span> <span class="nx">m</span><span class="p">(){</span><span class="kd">var</span> <span class="nx">a</span><span class="o">=</span><span class="nx">e</span><span class="p">.</span><span class="nx">elements</span><span class="p">;</span><span class="k">return</span><span class="s2">&quot;string&quot;</span><span class="o">==</span><span class="k">typeof</span> <span class="nx">a</span><span class="o">?</span><span class="nx">a</span><span class="p">.</span><span class="nx">split</span><span class="p">(</span><span class="s2">&quot; &quot;</span><span class="p">)</span><span class="o">:</span><span class="nx">a</span><span class="p">}</span><span class="kd">function</span> <span class="nx">i</span><span class="p">(</span><span class="nx">a</span><span class="p">){</span><span class="kd">var</span> <span class="nx">b</span><span class="o">=</span><span class="nx">n</span><span class="p">[</span><span class="nx">a</span><span class="p">[</span><span class="nx">o</span><span class="p">]];</span><span class="nx">b</span><span class="o">||</span><span class="p">(</span><span class="nx">b</span><span class="o">=</span><span class="p">{},</span><span class="nx">h</span><span class="o">++</span><span class="p">,</span><span class="nx">a</span><span class="p">[</span><span class="nx">o</span><span class="p">]</span><span class="o">=</span><span class="nx">h</span><span class="p">,</span><span class="nx">n</span><span class="p">[</span><span class="nx">h</span><span class="p">]</span><span class="o">=</span><span class="nx">b</span><span class="p">);</span><span class="k">return</span> <span class="nx">b</span><span class="p">}</span><span class="kd">function</span> <span class="nx">p</span><span class="p">(</span><span class="nx">a</span><span class="p">,</span><span class="nx">b</span><span class="p">,</span><span class="nx">c</span><span class="p">){</span><span class="nx">b</span><span class="o">||</span><span class="p">(</span><span class="nx">b</span><span class="o">=</span><span class="nx">f</span><span class="p">);</span><span class="k">if</span><span class="p">(</span><span class="nx">g</span><span class="p">)</span><span class="k">return</span> <span class="nx">b</span><span class="p">.</span><span class="nx">createElement</span><span class="p">(</span><span class="nx">a</span><span class="p">);</span><span class="nx">c</span><span class="o">||</span><span class="p">(</span><span class="nx">c</span><span class="o">=</span><span class="nx">i</span><span class="p">(</span><span class="nx">b</span><span class="p">));</span><span class="nx">b</span><span class="o">=</span><span class="nx">c</span><span class="p">.</span><span class="nx">cache</span><span class="p">[</span><span class="nx">a</span><span class="p">]</span><span class="o">?</span><span class="nx">c</span><span class="p">.</span><span class="nx">cache</span><span class="p">[</span><span class="nx">a</span><span class="p">].</span><span class="nx">cloneNode</span><span class="p">()</span><span class="o">:</span><span class="nx">r</span><span class="p">.</span><span class="nx">test</span><span class="p">(</span><span class="nx">a</span><span class="p">)</span><span class="o">?</span><span class="p">(</span><span class="nx">c</span><span class="p">.</span><span class="nx">cache</span><span class="p">[</span><span class="nx">a</span><span class="p">]</span><span class="o">=</span><span class="nx">c</span><span class="p">.</span><span class="nx">createElem</span><span class="p">(</span><span class="nx">a</span><span class="p">)).</span><span class="nx">cloneNode</span><span class="p">()</span><span class="o">:</span><span class="nx">c</span><span class="p">.</span><span class="nx">createElem</span><span class="p">(</span><span class="nx">a</span><span class="p">);</span><span class="k">return</span> <span class="nx">b</span><span class="p">.</span><span class="nx">canHaveChildren</span><span class="o">&amp;&amp;!</span><span class="nx">s</span><span class="p">.</span><span class="nx">test</span><span class="p">(</span><span class="nx">a</span><span class="p">)</span><span class="o">?</span><span class="nx">c</span><span class="p">.</span><span class="nx">frag</span><span class="p">.</span><span class="nx">appendChild</span><span class="p">(</span><span class="nx">b</span><span class="p">)</span><span class="o">:</span><span class="nx">b</span><span class="p">}</span><span class="kd">function</span> <span class="nx">t</span><span class="p">(</span><span class="nx">a</span><span class="p">,</span><span class="nx">b</span><span class="p">){</span><span class="k">if</span><span class="p">(</span><span class="o">!</span><span class="nx">b</span><span class="p">.</span><span class="nx">cache</span><span class="p">)</span><span class="nx">b</span><span class="p">.</span><span class="nx">cache</span><span class="o">=</span><span class="p">{},</span><span class="nx">b</span><span class="p">.</span><span class="nx">createElem</span><span class="o">=</span><span class="nx">a</span><span class="p">.</span><span class="nx">createElement</span><span class="p">,</span><span class="nx">b</span><span class="p">.</span><span class="nx">createFrag</span><span class="o">=</span><span class="nx">a</span><span class="p">.</span><span class="nx">createDocumentFragment</span><span class="p">,</span><span class="nx">b</span><span class="p">.</span><span class="nx">frag</span><span class="o">=</span><span class="nx">b</span><span class="p">.</span><span class="nx">createFrag</span><span class="p">();</span></div><div class='line' id='LC5'><span class="nx">a</span><span class="p">.</span><span class="nx">createElement</span><span class="o">=</span><span class="kd">function</span><span class="p">(</span><span class="nx">c</span><span class="p">){</span><span class="k">return</span><span class="o">!</span><span class="nx">e</span><span class="p">.</span><span class="nx">shivMethods</span><span class="o">?</span><span class="nx">b</span><span class="p">.</span><span class="nx">createElem</span><span class="p">(</span><span class="nx">c</span><span class="p">)</span><span class="o">:</span><span class="nx">p</span><span class="p">(</span><span class="nx">c</span><span class="p">,</span><span class="nx">a</span><span class="p">,</span><span class="nx">b</span><span class="p">)};</span><span class="nx">a</span><span class="p">.</span><span class="nx">createDocumentFragment</span><span class="o">=</span><span class="nb">Function</span><span class="p">(</span><span class="s2">&quot;h,f&quot;</span><span class="p">,</span><span class="s2">&quot;return function(){var n=f.cloneNode(),c=n.createElement;h.shivMethods&amp;&amp;(&quot;</span><span class="o">+</span><span class="nx">m</span><span class="p">().</span><span class="nx">join</span><span class="p">().</span><span class="nx">replace</span><span class="p">(</span><span class="sr">/\w+/g</span><span class="p">,</span><span class="kd">function</span><span class="p">(</span><span class="nx">a</span><span class="p">){</span><span class="nx">b</span><span class="p">.</span><span class="nx">createElem</span><span class="p">(</span><span class="nx">a</span><span class="p">);</span><span class="nx">b</span><span class="p">.</span><span class="nx">frag</span><span class="p">.</span><span class="nx">createElement</span><span class="p">(</span><span class="nx">a</span><span class="p">);</span><span class="k">return</span><span class="s1">&#39;c(&quot;&#39;</span><span class="o">+</span><span class="nx">a</span><span class="o">+</span><span class="s1">&#39;&quot;)&#39;</span><span class="p">})</span><span class="o">+</span><span class="s2">&quot;);return n}&quot;</span><span class="p">)(</span><span class="nx">e</span><span class="p">,</span><span class="nx">b</span><span class="p">.</span><span class="nx">frag</span><span class="p">)}</span><span class="kd">function</span> <span class="nx">q</span><span class="p">(</span><span class="nx">a</span><span class="p">){</span><span class="nx">a</span><span class="o">||</span><span class="p">(</span><span class="nx">a</span><span class="o">=</span><span class="nx">f</span><span class="p">);</span><span class="kd">var</span> <span class="nx">b</span><span class="o">=</span><span class="nx">i</span><span class="p">(</span><span class="nx">a</span><span class="p">);</span><span class="k">if</span><span class="p">(</span><span class="nx">e</span><span class="p">.</span><span class="nx">shivCSS</span><span class="o">&amp;&amp;!</span><span class="nx">j</span><span class="o">&amp;&amp;!</span><span class="nx">b</span><span class="p">.</span><span class="nx">hasCSS</span><span class="p">){</span><span class="kd">var</span> <span class="nx">c</span><span class="p">,</span><span class="nx">d</span><span class="o">=</span><span class="nx">a</span><span class="p">;</span><span class="nx">c</span><span class="o">=</span><span class="nx">d</span><span class="p">.</span><span class="nx">createElement</span><span class="p">(</span><span class="s2">&quot;p&quot;</span><span class="p">);</span><span class="nx">d</span><span class="o">=</span><span class="nx">d</span><span class="p">.</span><span class="nx">getElementsByTagName</span><span class="p">(</span><span class="s2">&quot;head&quot;</span><span class="p">)[</span><span class="mi">0</span><span class="p">]</span><span class="o">||</span><span class="nx">d</span><span class="p">.</span><span class="nx">documentElement</span><span class="p">;</span><span class="nx">c</span><span class="p">.</span><span class="nx">innerHTML</span><span class="o">=</span><span class="s2">&quot;x&lt;style&gt;article,aside,figcaption,figure,footer,header,hgroup,nav,section{display:block}mark{background:#FF0;color:#000}&lt;/style&gt;&quot;</span><span class="p">;</span></div><div class='line' id='LC6'><span class="nx">c</span><span class="o">=</span><span class="nx">d</span><span class="p">.</span><span class="nx">insertBefore</span><span class="p">(</span><span class="nx">c</span><span class="p">.</span><span class="nx">lastChild</span><span class="p">,</span><span class="nx">d</span><span class="p">.</span><span class="nx">firstChild</span><span class="p">);</span><span class="nx">b</span><span class="p">.</span><span class="nx">hasCSS</span><span class="o">=!!</span><span class="nx">c</span><span class="p">}</span><span class="nx">g</span><span class="o">||</span><span class="nx">t</span><span class="p">(</span><span class="nx">a</span><span class="p">,</span><span class="nx">b</span><span class="p">);</span><span class="k">return</span> <span class="nx">a</span><span class="p">}</span><span class="kd">var</span> <span class="nx">k</span><span class="o">=</span><span class="nx">l</span><span class="p">.</span><span class="nx">html5</span><span class="o">||</span><span class="p">{},</span><span class="nx">s</span><span class="o">=</span><span class="sr">/^&lt;|^(?:button|map|select|textarea|object|iframe|option|optgroup)$/i</span><span class="p">,</span><span class="nx">r</span><span class="o">=</span><span class="sr">/^(?:a|b|code|div|fieldset|h1|h2|h3|h4|h5|h6|i|label|li|ol|p|q|span|strong|style|table|tbody|td|th|tr|ul)$/i</span><span class="p">,</span><span class="nx">j</span><span class="p">,</span><span class="nx">o</span><span class="o">=</span><span class="s2">&quot;_html5shiv&quot;</span><span class="p">,</span><span class="nx">h</span><span class="o">=</span><span class="mi">0</span><span class="p">,</span><span class="nx">n</span><span class="o">=</span><span class="p">{},</span><span class="nx">g</span><span class="p">;(</span><span class="kd">function</span><span class="p">(){</span><span class="k">try</span><span class="p">{</span><span class="kd">var</span> <span class="nx">a</span><span class="o">=</span><span class="nx">f</span><span class="p">.</span><span class="nx">createElement</span><span class="p">(</span><span class="s2">&quot;a&quot;</span><span class="p">);</span><span class="nx">a</span><span class="p">.</span><span class="nx">innerHTML</span><span class="o">=</span><span class="s2">&quot;&lt;xyz&gt;&lt;/xyz&gt;&quot;</span><span class="p">;</span><span class="nx">j</span><span class="o">=</span><span class="s2">&quot;hidden&quot;</span><span class="k">in</span> <span class="nx">a</span><span class="p">;</span><span class="kd">var</span> <span class="nx">b</span><span class="p">;</span><span class="k">if</span><span class="p">(</span><span class="o">!</span><span class="p">(</span><span class="nx">b</span><span class="o">=</span><span class="mi">1</span><span class="o">==</span><span class="nx">a</span><span class="p">.</span><span class="nx">childNodes</span><span class="p">.</span><span class="nx">length</span><span class="p">)){</span><span class="nx">f</span><span class="p">.</span><span class="nx">createElement</span><span class="p">(</span><span class="s2">&quot;a&quot;</span><span class="p">);</span><span class="kd">var</span> <span class="nx">c</span><span class="o">=</span><span class="nx">f</span><span class="p">.</span><span class="nx">createDocumentFragment</span><span class="p">();</span><span class="nx">b</span><span class="o">=</span><span class="s2">&quot;undefined&quot;</span><span class="o">==</span><span class="k">typeof</span> <span class="nx">c</span><span class="p">.</span><span class="nx">cloneNode</span><span class="o">||</span></div><div class='line' id='LC7'><span class="s2">&quot;undefined&quot;</span><span class="o">==</span><span class="k">typeof</span> <span class="nx">c</span><span class="p">.</span><span class="nx">createDocumentFragment</span><span class="o">||</span><span class="s2">&quot;undefined&quot;</span><span class="o">==</span><span class="k">typeof</span> <span class="nx">c</span><span class="p">.</span><span class="nx">createElement</span><span class="p">}</span><span class="nx">g</span><span class="o">=</span><span class="nx">b</span><span class="p">}</span><span class="k">catch</span><span class="p">(</span><span class="nx">d</span><span class="p">){</span><span class="nx">g</span><span class="o">=</span><span class="nx">j</span><span class="o">=!</span><span class="mi">0</span><span class="p">}})();</span><span class="kd">var</span> <span class="nx">e</span><span class="o">=</span><span class="p">{</span><span class="nx">elements</span><span class="o">:</span><span class="nx">k</span><span class="p">.</span><span class="nx">elements</span><span class="o">||</span><span class="s2">&quot;abbr article aside audio bdi canvas data datalist details figcaption figure footer header hgroup mark meter nav output progress section summary time video&quot;</span><span class="p">,</span><span class="nx">version</span><span class="o">:</span><span class="s2">&quot;3.6.2pre&quot;</span><span class="p">,</span><span class="nx">shivCSS</span><span class="o">:!</span><span class="mi">1</span><span class="o">!==</span><span class="nx">k</span><span class="p">.</span><span class="nx">shivCSS</span><span class="p">,</span><span class="nx">supportsUnknownElements</span><span class="o">:</span><span class="nx">g</span><span class="p">,</span><span class="nx">shivMethods</span><span class="o">:!</span><span class="mi">1</span><span class="o">!==</span><span class="nx">k</span><span class="p">.</span><span class="nx">shivMethods</span><span class="p">,</span><span class="nx">type</span><span class="o">:</span><span class="s2">&quot;default&quot;</span><span class="p">,</span><span class="nx">shivDocument</span><span class="o">:</span><span class="nx">q</span><span class="p">,</span><span class="nx">createElement</span><span class="o">:</span><span class="nx">p</span><span class="p">,</span><span class="nx">createDocumentFragment</span><span class="o">:</span><span class="kd">function</span><span class="p">(</span><span class="nx">a</span><span class="p">,</span><span class="nx">b</span><span class="p">){</span><span class="nx">a</span><span class="o">||</span><span class="p">(</span><span class="nx">a</span><span class="o">=</span><span class="nx">f</span><span class="p">);</span><span class="k">if</span><span class="p">(</span><span class="nx">g</span><span class="p">)</span><span class="k">return</span> <span class="nx">a</span><span class="p">.</span><span class="nx">createDocumentFragment</span><span class="p">();</span></div><div class='line' id='LC8'><span class="k">for</span><span class="p">(</span><span class="kd">var</span> <span class="nx">b</span><span class="o">=</span><span class="nx">b</span><span class="o">||</span><span class="nx">i</span><span class="p">(</span><span class="nx">a</span><span class="p">),</span><span class="nx">c</span><span class="o">=</span><span class="nx">b</span><span class="p">.</span><span class="nx">frag</span><span class="p">.</span><span class="nx">cloneNode</span><span class="p">(),</span><span class="nx">d</span><span class="o">=</span><span class="mi">0</span><span class="p">,</span><span class="nx">e</span><span class="o">=</span><span class="nx">m</span><span class="p">(),</span><span class="nx">h</span><span class="o">=</span><span class="nx">e</span><span class="p">.</span><span class="nx">length</span><span class="p">;</span><span class="nx">d</span><span class="o">&lt;</span><span class="nx">h</span><span class="p">;</span><span class="nx">d</span><span class="o">++</span><span class="p">)</span><span class="nx">c</span><span class="p">.</span><span class="nx">createElement</span><span class="p">(</span><span class="nx">e</span><span class="p">[</span><span class="nx">d</span><span class="p">]);</span><span class="k">return</span> <span class="nx">c</span><span class="p">}};</span><span class="nx">l</span><span class="p">.</span><span class="nx">html5</span><span class="o">=</span><span class="nx">e</span><span class="p">;</span><span class="nx">q</span><span class="p">(</span><span class="nx">f</span><span class="p">)})(</span><span class="k">this</span><span class="p">,</span><span class="nb">document</span><span class="p">);</span></div></pre></div>
          </td>
        </tr>
      </table>
  </div>

  </div>
</div>

<a href="#jump-to-line" rel="facebox[.linejump]" data-hotkey="l" class="js-jump-to-line" style="display:none">Jump to Line</a>
<div id="jump-to-line" style="display:none">
  <form accept-charset="UTF-8" class="js-jump-to-line-form">
    <input class="linejump-input js-jump-to-line-field" type="text" placeholder="Jump to line&hellip;" autofocus>
    <button type="submit" class="button">Go</button>
  </form>
</div>

        </div>

      </div><!-- /.repo-container -->
      <div class="modal-backdrop"></div>
    </div><!-- /.container -->
  </div><!-- /.site -->


    </div><!-- /.wrapper -->

      <div class="container">
  <div class="site-footer">
    <ul class="site-footer-links right">
      <li><a href="https://status.github.com/">Status</a></li>
      <li><a href="http://developer.github.com">API</a></li>
      <li><a href="http://training.github.com">Training</a></li>
      <li><a href="http://shop.github.com">Shop</a></li>
      <li><a href="/blog">Blog</a></li>
      <li><a href="/about">About</a></li>

    </ul>

    <a href="/">
      <span class="mega-octicon octicon-mark-github"></span>
    </a>

    <ul class="site-footer-links">
      <li>&copy; 2013 <span title="0.11971s from fe2.rs.github.com">GitHub</span>, Inc.</li>
        <li><a href="/site/terms">Terms</a></li>
        <li><a href="/site/privacy">Privacy</a></li>
        <li><a href="/security">Security</a></li>
        <li><a href="/contact">Contact</a></li>
    </ul>
  </div><!-- /.site-footer -->
</div><!-- /.container -->


    <div class="fullscreen-overlay js-fullscreen-overlay" id="fullscreen_overlay">
  <div class="fullscreen-container js-fullscreen-container">
    <div class="textarea-wrap">
      <textarea name="fullscreen-contents" id="fullscreen-contents" class="js-fullscreen-contents" placeholder="" data-suggester="fullscreen_suggester"></textarea>
          <div class="suggester-container">
              <div class="suggester fullscreen-suggester js-navigation-container" id="fullscreen_suggester"
                 data-url="/twbs/bootstrap/suggestions/commit">
              </div>
          </div>
    </div>
  </div>
  <div class="fullscreen-sidebar">
    <a href="#" class="exit-fullscreen js-exit-fullscreen tooltipped leftwards" title="Exit Zen Mode">
      <span class="mega-octicon octicon-screen-normal"></span>
    </a>
    <a href="#" class="theme-switcher js-theme-switcher tooltipped leftwards"
      title="Switch themes">
      <span class="octicon octicon-color-mode"></span>
    </a>
  </div>
</div>



    <div id="ajax-error-message" class="flash flash-error">
      <span class="octicon octicon-alert"></span>
      <a href="#" class="octicon octicon-remove-close close ajax-error-dismiss"></a>
      Something went wrong with that request. Please try again.
    </div>

    
  </body>
</html>

