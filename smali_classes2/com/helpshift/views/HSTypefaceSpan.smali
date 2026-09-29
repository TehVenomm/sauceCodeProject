.class public Lcom/helpshift/views/HSTypefaceSpan;
.super Landroid/text/style/MetricAffectingSpan;
.source "HSTypefaceSpan.java"


# instance fields
.field private final typeface:Landroid/graphics/Typeface;


# direct methods
.method constructor <init>(Landroid/graphics/Typeface;)V
    .locals 0

    .line 11
    invoke-direct {p0}, Landroid/text/style/MetricAffectingSpan;-><init>()V

    .line 12
    iput-object p1, p0, Lcom/helpshift/views/HSTypefaceSpan;->typeface:Landroid/graphics/Typeface;

    return-void
.end method


# virtual methods
.method public updateDrawState(Landroid/text/TextPaint;)V
    .locals 1

    .line 22
    iget-object v0, p0, Lcom/helpshift/views/HSTypefaceSpan;->typeface:Landroid/graphics/Typeface;

    invoke-virtual {p1, v0}, Landroid/text/TextPaint;->setTypeface(Landroid/graphics/Typeface;)Landroid/graphics/Typeface;

    return-void
.end method

.method public updateMeasureState(Landroid/text/TextPaint;)V
    .locals 1

    .line 17
    iget-object v0, p0, Lcom/helpshift/views/HSTypefaceSpan;->typeface:Landroid/graphics/Typeface;

    invoke-virtual {p1, v0}, Landroid/text/TextPaint;->setTypeface(Landroid/graphics/Typeface;)Landroid/graphics/Typeface;

    return-void
.end method
