.class final Lcom/helpshift/util/HSLinkify$4;
.super Landroid/text/style/URLSpan;
.source "HSLinkify.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/util/HSLinkify;->addLinks(Landroid/text/Spannable;Ljava/util/regex/Pattern;Ljava/lang/String;Lcom/helpshift/util/HSLinkify$MatchFilter;Lcom/helpshift/util/HSLinkify$TransformFilter;Lcom/helpshift/util/HSLinkify$LinkClickListener;)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$linkClickListener:Lcom/helpshift/util/HSLinkify$LinkClickListener;

.field final synthetic val$url:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;Lcom/helpshift/util/HSLinkify$LinkClickListener;Ljava/lang/String;)V
    .locals 0

    .line 518
    iput-object p2, p0, Lcom/helpshift/util/HSLinkify$4;->val$linkClickListener:Lcom/helpshift/util/HSLinkify$LinkClickListener;

    iput-object p3, p0, Lcom/helpshift/util/HSLinkify$4;->val$url:Ljava/lang/String;

    invoke-direct {p0, p1}, Landroid/text/style/URLSpan;-><init>(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 522
    invoke-super {p0, p1}, Landroid/text/style/URLSpan;->onClick(Landroid/view/View;)V

    .line 525
    iget-object p1, p0, Lcom/helpshift/util/HSLinkify$4;->val$linkClickListener:Lcom/helpshift/util/HSLinkify$LinkClickListener;

    if-eqz p1, :cond_0

    .line 526
    iget-object p1, p0, Lcom/helpshift/util/HSLinkify$4;->val$linkClickListener:Lcom/helpshift/util/HSLinkify$LinkClickListener;

    iget-object v0, p0, Lcom/helpshift/util/HSLinkify$4;->val$url:Ljava/lang/String;

    invoke-interface {p1, v0}, Lcom/helpshift/util/HSLinkify$LinkClickListener;->onLinkClicked(Ljava/lang/String;)V

    :cond_0
    return-void
.end method
