.class final Lcom/helpshift/util/HSLinkify$3;
.super Landroid/text/style/URLSpan;
.source "HSLinkify.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/util/HSLinkify;->addLinks(Landroid/text/Spannable;ILcom/helpshift/util/HSLinkify$LinkClickListener;)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$link:Lcom/helpshift/util/LinkSpec;

.field final synthetic val$linkClickListener:Lcom/helpshift/util/HSLinkify$LinkClickListener;


# direct methods
.method constructor <init>(Ljava/lang/String;Lcom/helpshift/util/HSLinkify$LinkClickListener;Lcom/helpshift/util/LinkSpec;)V
    .locals 0

    .line 355
    iput-object p2, p0, Lcom/helpshift/util/HSLinkify$3;->val$linkClickListener:Lcom/helpshift/util/HSLinkify$LinkClickListener;

    iput-object p3, p0, Lcom/helpshift/util/HSLinkify$3;->val$link:Lcom/helpshift/util/LinkSpec;

    invoke-direct {p0, p1}, Landroid/text/style/URLSpan;-><init>(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 359
    invoke-super {p0, p1}, Landroid/text/style/URLSpan;->onClick(Landroid/view/View;)V

    .line 362
    iget-object p1, p0, Lcom/helpshift/util/HSLinkify$3;->val$linkClickListener:Lcom/helpshift/util/HSLinkify$LinkClickListener;

    if-eqz p1, :cond_0

    .line 363
    iget-object p1, p0, Lcom/helpshift/util/HSLinkify$3;->val$linkClickListener:Lcom/helpshift/util/HSLinkify$LinkClickListener;

    iget-object v0, p0, Lcom/helpshift/util/HSLinkify$3;->val$link:Lcom/helpshift/util/LinkSpec;

    iget-object v0, v0, Lcom/helpshift/util/LinkSpec;->url:Ljava/lang/String;

    invoke-interface {p1, v0}, Lcom/helpshift/util/HSLinkify$LinkClickListener;->onLinkClicked(Ljava/lang/String;)V

    :cond_0
    return-void
.end method
